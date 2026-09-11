using MySqlConnector;
using spikewall.Response;

namespace spikewall.Object
{
    public class LeaderboardOperations
    {
        public static SRStatusCode GetEndlessHighScores(MySqlConnection conn, string uid)
        {
            PlayerState playerState = new();
            var populateStatus = playerState.Populate(conn, uid);
            if (populateStatus != SRStatusCode.Ok)
            {
                return populateStatus;
            }

            var endlessHighScoresSql = Db.GetCommand("SELECT * FROM `sw_players` ORDER BY story_high_score DESC");
            var endlessHighScoresCommand = new MySqlCommand(endlessHighScoresSql, conn);
            endlessHighScoresCommand.ExecuteNonQuery();

            return SRStatusCode.Ok;
        }

        public static SRStatusCode GetQuickHighScores(MySqlConnection conn, string uid)
        {
            PlayerState playerState = new();
            var populateStatus = playerState.Populate(conn, uid);
            if (populateStatus != SRStatusCode.Ok)
            {
                return populateStatus;
            }

            var quickHighScoresSql = Db.GetCommand("SELECT * FROM `sw_players` ORDER BY quick_high_score DESC");
            var quickHighScoresCommand = new MySqlCommand(quickHighScoresSql, conn);
            quickHighScoresCommand.ExecuteNonQuery();

            return SRStatusCode.Ok;
        }

        public static SRStatusCode GetEndlessLeagueHighScores(MySqlConnection conn, string uid)
        {
            PlayerState playerState = new();
            var populateStatus = playerState.Populate(conn, uid);
            if (populateStatus != SRStatusCode.Ok)
            {
                return populateStatus;
            }

            var endlessLeagueHighScoresSql = Db.GetCommand("SELECT * FROM `sw_players` ORDER BY league_high_score DESC");
            var endlessLeagueHighScoresCommand = new MySqlCommand(endlessLeagueHighScoresSql, conn);
            endlessLeagueHighScoresCommand.ExecuteNonQuery();

            return SRStatusCode.Ok;
        }
        public static SRStatusCode GetQuickLeagueHighScores(MySqlConnection conn, string uid)
        {
            PlayerState playerState = new();
            var populateStatus = playerState.Populate(conn, uid);
            if (populateStatus != SRStatusCode.Ok)
            {
                return populateStatus;
            }

            var quickLeagueHighScoresSql = Db.GetCommand("SELECT * FROM `sw_players` ORDER BY quick_league_high_score DESC");
            var quickLeagueHighScoresCommand = new MySqlCommand(quickLeagueHighScoresSql, conn);
            quickLeagueHighScoresCommand.ExecuteNonQuery();

            return SRStatusCode.Ok;
        }

        public static SRStatusCode GetNumberOfPlayers(MySqlConnection conn, out long numberofPlayers)
        {
            var numberofPlayersSql = Db.GetCommand(@"SELECT COUNT(id) FROM `sw_players`");
            numberofPlayers = Convert.ToInt64(numberofPlayersSql);
            return SRStatusCode.Ok;
        }

        public static SRStatusCode GetNumberOfEndlessRunnersLeaguePlayers(MySqlConnection conn, out long numberOfEndlessRunnersLeaguePlayers)
        {
            LeagueData leagueData = new();

            var numberofEndlessRunnersLeaguePlayersSql = Db.GetCommand(@"SELECT COUNT(id) FROM `sw_players` ORDER BY league_high_score DESC, story_total_score DESC WHERE quick_ranking_league = '{0}' AND ranking_league_group ='{1}'", leagueData.leagueId, leagueData.groupId);
            numberOfEndlessRunnersLeaguePlayers = Convert.ToInt64(numberofEndlessRunnersLeaguePlayersSql);
            return SRStatusCode.Ok;
        }
        public static SRStatusCode GetNumberOfQuickRunnersLeaguePlayers(MySqlConnection conn, out long numberOfQuickRunnersLeaguePlayers)
        {
            LeagueData leagueData = new();

            var numberofQuickRunnersLeaguePlayersSql = Db.GetCommand(@"SELECT COUNT(id) FROM `sw_players` ORDER BY quick_league_high_score DESC, quick_total_score DESC WHERE quick_ranking_league = '{0}' AND quick_ranking_league_group ='{1}'", leagueData.leagueId, leagueData.groupId);
            numberOfQuickRunnersLeaguePlayers = Convert.ToInt64(numberofQuickRunnersLeaguePlayersSql);
            return SRStatusCode.Ok;
        }
    }
}
