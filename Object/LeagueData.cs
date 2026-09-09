using MySqlConnector;
using spikewall.Response;
using static spikewall.Object.Character;

namespace spikewall.Object
{
    public class LeagueData
    {
        public long? leagueId { get; set; }
        public long? groupId { get; set; }
        public long? numUp { get; set; }
        public long? numDown { get; set; }
        public long? numGroupMember { get; set; }
        public long? numLeagueMember { get; set; }
        public OperatorScore[]? highScoreOpe { get; set; }
        public OperatorScore[]? totalScoreOpe { get; set; }

        /// <summary>
        /// Enum that contains the names 
        /// and IDs of all Runners League Ranks.
        /// The number at the end signifies the number of stars in each League Rank.
        /// </summary>
        public enum LeagueID
        {
            F1,
            F2,
            F3,
            E1,
            E2,
            E3,
            D1,
            D2,
            D3,
            C1,
            C2,
            C3,
            B1,
            B2,
            B3,
            A1,
            A2,
            A3,
            S1,
            S2,
            S3
        }

        /// <summary>
        /// Enum that contains the the mode types
        /// and IDs of the Runners League Ranks.
        /// </summary>
        public enum RankingMode
        {
            Endless,
            Quick
        }

        public static SRStatusCode GenerateEndlessLeagueData(MySqlConnection conn, out LeagueData[] endlessLeague)
        {
            List<LeagueData> endlessLeagueDataList = new List<LeagueData>();

            var generateEndlessLeagueDataSql = Db.GetCommand(@"SELECT * FROM `sw_endlessleaguedata`");
            var generateEndlessLeagueDataCmd = new MySqlCommand(generateEndlessLeagueDataSql, conn);
            var generateEndlessLeagueDataReader = generateEndlessLeagueDataCmd.ExecuteReader();

            while (generateEndlessLeagueDataReader.Read())
            {
                LeagueData endlessLeagueData = new();
                endlessLeagueData.leagueId = Convert.ToInt64(generateEndlessLeagueDataReader["league_id"]);
                endlessLeagueData.groupId = Convert.ToInt64(generateEndlessLeagueDataReader["group_id"]);
                endlessLeagueData.numUp = Convert.ToInt64(generateEndlessLeagueDataReader["num_up"]);
                endlessLeagueData.numDown = Convert.ToInt64(generateEndlessLeagueDataReader["num_down"]);
                endlessLeagueData.numGroupMember = Convert.ToInt64(generateEndlessLeagueDataReader["num_in_group"]);
                endlessLeagueData.numLeagueMember = Convert.ToInt64(generateEndlessLeagueDataReader["num_in_league"]);
                endlessLeagueData.highScoreOpe = OperatorScore.GenerateEndlessLeagueHighScorePrizes(conn, endlessLeagueData.leagueId);
                endlessLeagueData.highScoreOpe = OperatorScore.GenerateEndlessLeagueTotalScorePrizes(conn, endlessLeagueData.leagueId);

                if (endlessLeagueData.numGroupMember > 50)
                {
                    endlessLeagueData.groupId += 1;
                    endlessLeagueData.numGroupMember = 0;
                    endlessLeagueData.numGroupMember += 1;
                }

                endlessLeagueDataList.Add(endlessLeagueData);
            }

            generateEndlessLeagueDataReader.Close();

            endlessLeague = endlessLeagueDataList.ToArray();

            return SRStatusCode.Ok;
        }

        public static SRStatusCode GenerateQuickLeagueData(MySqlConnection conn, out LeagueData[] quickLeague)
        {
            List<LeagueData> quickLeagueDataList = new List<LeagueData>();

            var generateQuickLeagueDataSql = Db.GetCommand(@"SELECT * FROM `sw_quickleaguedata`");
            var generateQuickLeagueDataCmd = new MySqlCommand(generateQuickLeagueDataSql, conn);
            var generateQuickLeagueDataReader = generateQuickLeagueDataCmd.ExecuteReader();

            while (generateQuickLeagueDataReader.Read())
            {
                LeagueData quickLeagueData = new();
                quickLeagueData.leagueId = Convert.ToInt64(generateQuickLeagueDataReader["league_id"]);
                quickLeagueData.groupId = Convert.ToInt64(generateQuickLeagueDataReader["group_id"]);
                quickLeagueData.numUp = Convert.ToInt64(generateQuickLeagueDataReader["num_up"]);
                quickLeagueData.numDown = Convert.ToInt64(generateQuickLeagueDataReader["num_down"]);
                quickLeagueData.numGroupMember = Convert.ToInt64(generateQuickLeagueDataReader["num_in_group"]);
                quickLeagueData.numLeagueMember = Convert.ToInt64(generateQuickLeagueDataReader["num_in_league"]);
                quickLeagueData.highScoreOpe = OperatorScore.GenerateQuickLeagueHighScorePrizes(conn, quickLeagueData.leagueId);
                quickLeagueData.highScoreOpe = OperatorScore.GenerateQuickLeagueTotalScorePrizes(conn, quickLeagueData.leagueId);

                if (quickLeagueData.numGroupMember > 50)
                {
                    quickLeagueData.groupId += 1;
                    quickLeagueData.numGroupMember = 0;
                    quickLeagueData.numGroupMember += 1;
                }

                quickLeagueDataList.Add(quickLeagueData);
            }

            generateQuickLeagueDataReader.Close();

            quickLeague = quickLeagueDataList.ToArray();

            return SRStatusCode.Ok;
        }

        public static SRStatusCode AddPlayerToEndlessLeagueState(MySqlConnection conn, string uid)
        {
            //get player id
            var getPlayerSql = Db.GetCommand(@"SELECT * FROM `sw_players` WHERE id = '{0}'", uid);
            var getPlayerCmd = new MySqlCommand(getPlayerSql, conn);
            var getPlayerRdr = getPlayerCmd.ExecuteReader();

            if (getPlayerRdr.HasRows)
            {
                //placeholder
                List<LeaderboardEntry> playerList = new List<LeaderboardEntry>();

                getPlayerRdr.Read();

                LeaderboardEntry playerEntry = new()
                {
                    friendId = Convert.ToString(getPlayerRdr["id"]),
                    name = Convert.ToString(getPlayerRdr["username"]),
                    numRank = Convert.ToInt64(getPlayerRdr["num_rank"]),

                };


                getPlayerRdr.Close();
            }

            return SRStatusCode.Ok;
        }

        //public LeagueData()
        //{
        //    leagueId = "0";
        //    groupId = "0";
        //    numUp = "40";
        //    numDown = "0";
        //    numGroupMember = "0";
        //    highScoreOpe = Array.Empty<OperatorScore>();
        //    totalScoreOpe = Array.Empty<OperatorScore>();
        //}
    }
}
