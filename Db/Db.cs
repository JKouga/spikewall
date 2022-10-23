using MySql.Data;
using MySql.Data.MySqlClient;

namespace spikewall
{
    public class Db
    {
        /// <summary>
        /// Initialize database details from locally-stored secrets
        /// </summary>
        /// MySQL connection details are stored as .NET secrets. To set these on the server (or
        /// locally), issue the following commands (replacing the second value as necessary):
        ///
        ///   dotnet user-secrets set "Db:Host" "localhost"
        ///   dotnet user-secrets set "Db:Port" "3306"
        ///   dotnet user-secrets set "Db:Username" "dbuser"
        ///   dotnet user-secrets set "Db:Password" "dbpass"
        ///   dotnet user-secrets set "Db:Database" "spikewall"
        ///
        public static void Initialize(ref WebApplicationBuilder builder)
        {
            dbHost = builder.Configuration["Db:Host"];
            dbUser = builder.Configuration["Db:Username"];
            dbPass = builder.Configuration["Db:Password"];
            dbName = builder.Configuration["Db:Database"];

            try
            {
                dbPort = Int16.Parse(builder.Configuration["Db:Port"]);
            }
            catch (ArgumentNullException)
            {
                dbPort = 0;
            }
        }

        /// <summary>
        /// Set up necessary database tables
        /// </summary>
        public static void SetupTables()
        {
            try
            {
                using (var conn = Get())
                {
                    conn.Open();

                    var cmd = new MySqlCommand(
                        @"CREATE TABLE IF NOT EXISTS `sw_players` (
                            id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
                            password VARCHAR(20) NOT NULL,
                            server_key VARCHAR(20) NOT NULL,

                            username VARCHAR(12) NOT NULL DEFAULT '',
                            migrate_password VARCHAR(12),
                            language INTEGER,
                            characters JSON,
                            chao JSON,
                            suspended_until BIGINT,
                            suspend_reason INTEGER,

                            last_login BIGINT,
                            last_login_device TEXT,
                            last_login_platform INTEGER,
                            last_login_version TEXT
                        );
                        ALTER TABLE `sw_players` AUTO_INCREMENT=1000000000;
                        CREATE TABLE IF NOT EXISTS `sw_sessions` (
                            sid VARCHAR(48) NOT NULL PRIMARY KEY,
                            uid BIGINT UNSIGNED NOT NULL,
                            time BIGINT NOT NULL
                        );
                        CREATE TABLE IF NOT EXISTS `sw_config` (
                            is_maintenance TINYINT NOT NULL,
                            support_legacy_versions TINYINT NOT NULL
                        );", conn);

                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Failed to set up tables: " + e.ToString());
            }
        }

        /// <summary>
        /// Retrieve valid MySQL connection to make queries with.
        /// </summary>
        ///
        /// This function will return a valid MySqlConnection, or pass a MySqlException if an error
        /// occurs. Using try/catch is recommended, at least in sections where the validity of
        /// database details is checked.
        ///
        /// This can also be used in a `using` statement, e.g. `using (var conn = Db.Get())`
        ///
        /// After calling this function, call `Open()` on the resulting object. Then queries can be
        /// made with MySqlCommand. Call `Close()` when you're finished (this might happen
        /// automatically with `using`, but it's probably good practice either way),
        public static MySqlConnection Get()
        {
            // Build MySQL connection string out of loaded parameters
            string connectionString = String.Format("server={0};user={1};database={2};port={3};password={4}",
                                                    dbHost, dbUser, dbName, dbPort, dbPass);

            // Return connection
            return new MySqlConnection(connectionString);
        }

        public static string EscapeString(string s)
        {
            return s.Replace("'", "\\'");
        }

        /// <summary>
        /// Generate an SQL string where all paramters are escaped (assumes single quotes are used for values)
        /// </summary>
        public static string GetCommand(string format, params object[] arg)
        {
            for (int i = 0; i < arg.Length; i++) {
                if (arg[i] is string) {
                    arg[i] = EscapeString((string) arg[i]);
                }
            }
            return string.Format(format, arg);
        }

        private static string dbHost = "";
        private static string dbUser = "";
        private static string dbPass = "";
        private static Int16 dbPort = 0;
        private static string dbName = "";
    }
}
