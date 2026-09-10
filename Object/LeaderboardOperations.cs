using MySqlConnector;
using spikewall.Response;

namespace spikewall.Object
{
    public class LeaderboardOperations
    {
        public static SRStatusCode GetEndlessHighScores(MySqlConnection conn, long lbtype, string uid)
        {
            PlayerState playerState = new();
            var populateStatus = playerState.Populate(conn, uid);
            if (populateStatus != SRStatusCode.Ok)
            {
                return populateStatus;
            }

            string sortingColumn = "";

            switch (lbtype)
            {
                case 0:
                case 2:
                case 4:
                    sortingColumn = "league_high_score";
                    break;
                case 1:
                case 3:
                case 5:
                    sortingColumn = "total_score";
                    break;
            }

            return SRStatusCode.Ok;
        }

        public static SRStatusCode GetQuickHighScores(MySqlConnection conn, string uid)
        {

            return SRStatusCode.Ok;
        }

        public static SRStatusCode GetEndlessLeagueHighScores(MySqlConnection conn, string uid)
        {
            return SRStatusCode.Ok;
        }
        public static SRStatusCode GetQuickLeagueHighScores(MySqlConnection conn, string uid)
        {
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

            var numberofEndlessRunnersLeaguePlayersSql = Db.GetCommand(@"SELECT COUNT(id) FROM `sw_players` WHERE quick_ranking_league = '{0}' AND ranking_league_group ='{1}'", leagueData.leagueId, leagueData.groupId);
            numberOfEndlessRunnersLeaguePlayers = Convert.ToInt64(numberofEndlessRunnersLeaguePlayersSql);
            return SRStatusCode.Ok;
        }
        public static SRStatusCode GetNumberOfQuickRunnersLeaguePlayers(MySqlConnection conn, out long numberOfQuickRunnersLeaguePlayers)
        {
            LeagueData leagueData = new();

            var numberofQuickRunnersLeaguePlayersSql = Db.GetCommand(@"SELECT COUNT(id) FROM `sw_players` WHERE quick_ranking_league = '{0}' AND quick_ranking_league_group ='{1}'", leagueData.leagueId, leagueData.groupId);
            numberOfQuickRunnersLeaguePlayers = Convert.ToInt64(numberofQuickRunnersLeaguePlayersSql);
            return SRStatusCode.Ok;
        }
    }
}
