using MySqlConnector;
using spikewall.Response;
using System.Security.Cryptography;
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

        public static SRStatusCode CalculateAndResetEndlessRunnersLeague(MySqlConnection conn, string uid)
        {
            PlayerState playerState = new();
            var populateStatus = playerState.Populate(conn, uid);
            if (populateStatus != SRStatusCode.Ok)
            {
                return populateStatus;
            }

            DateTimeOffset leagueReset = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                0, 0, 0, 0).AddDays(7);

            if (DateTime.Now >= leagueReset)
            {
                LeagueData endlessLeague = new();
                var generateEndlessLeagueStatus = GenerateEndlessLeagueData(conn, out LeagueData[] endlessLeagueList);
                if (generateEndlessLeagueStatus != SRStatusCode.Ok)
                {
                    return generateEndlessLeagueStatus;
                }

                var endlessLeagueSql = Db.GetCommand(@"SELECT * FROM `sw_endlessleaguedata` WHERE league_id = '{0}'", endlessLeagueList[Convert.ToInt32(endlessLeague.leagueId)]);
                var endlessLeagueCommand = new MySqlCommand(endlessLeagueSql, conn);
                var endlessLeagueReader = endlessLeagueCommand.ExecuteReader();

                if (endlessLeagueReader.HasRows)
                {
                    endlessLeague.leagueId = Convert.ToInt64(endlessLeagueReader["league_id"]);
                    endlessLeague.groupId = Convert.ToInt64(endlessLeagueReader["group_id"]);
                    endlessLeague.numUp = Convert.ToInt64(endlessLeagueReader["num_up"]);
                    endlessLeague.numDown = Convert.ToInt64(endlessLeagueReader["num_down"]);
                    endlessLeague.numGroupMember = Convert.ToInt64(endlessLeagueReader["num_in_group"]);
                    endlessLeague.numLeagueMember = Convert.ToInt64(endlessLeagueReader["num_in_league"]);
                    endlessLeague.highScoreOpe = OperatorScore.GenerateEndlessLeagueHighScorePrizes(conn, endlessLeague.leagueId);
                    endlessLeague.highScoreOpe = OperatorScore.GenerateEndlessLeagueTotalScorePrizes(conn, endlessLeague.leagueId);

                    var playerSql = Db.GetCommand(@"SELECT * FROM `sw_player` WHERE id='{0}' ORDER BY ranking_league_group, ranking_league_group_id, story_total_score DESC", uid);
                    var playerCommand = new MySqlCommand(playerSql, conn);
                    var playerReader = playerCommand.ExecuteReader();

                    if (playerReader.HasRows)
                    {
                        playerState.rankingLeagueGroup = Convert.ToInt64(playerReader["ranking_league_group"]);
                        playerState.rankingLeague = Convert.ToInt64(playerReader["ranking_league"]);
                        if (playerState.rankingLeagueGroup <= endlessLeague.numUp)
                        {
                            endlessLeague.leagueId += 1;
                        }
                        else if (endlessLeague.leagueId >= 9 && playerState.rankingLeagueGroup > (endlessLeague.numGroupMember - endlessLeague.numDown))
                        {
                            endlessLeague.leagueId -= 1;
                        }
                        playerState.rankingLeague = endlessLeague.leagueId;
                    }

                    playerReader.Close();
                }

                endlessLeagueReader.Close();
            }

            var updatePlayerStateSql = Db.GetCommand(@"UPDATE `sw_players` SET ranking_league = '{0}' WHERE id= '{1}'",  playerState.rankingLeague, uid);
            var updatePlayerStateCommand = new MySqlCommand(updatePlayerStateSql, conn);
            updatePlayerStateCommand.ExecuteNonQuery();

            conn.Close();

            return SRStatusCode.Ok;
        }
        public static SRStatusCode CalculateAndResetQuickRunnersLeague(MySqlConnection conn, string uid)
        {

            PlayerState playerState = new();
            var populateStatus = playerState.Populate(conn, uid);
            if (populateStatus != SRStatusCode.Ok)
            {
                return populateStatus;
            }

            DateTimeOffset leagueReset = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                0, 0, 0, 0).AddDays(7);

            if (DateTime.Now >= leagueReset)
            {
                LeagueData quickLeague = new();
                var generateQuickLeagueStatus = GenerateQuickLeagueData(conn, out LeagueData[] quickLeagueList);
                if (generateQuickLeagueStatus != SRStatusCode.Ok)
                {
                    return generateQuickLeagueStatus;
                }

                var quickLeagueSql = Db.GetCommand(@"SELECT * FROM `sw_quickleaguedata` WHERE league_id = '{0}'", quickLeagueList[Convert.ToInt32(quickLeague.leagueId)]);
                var quickLeagueCommand = new MySqlCommand(quickLeagueSql, conn);
                var quickLeagueReader = quickLeagueCommand.ExecuteReader();

                if (quickLeagueReader.HasRows)
                {
                    quickLeague.leagueId = Convert.ToInt64(quickLeagueReader["league_id"]);
                    quickLeague.groupId = Convert.ToInt64(quickLeagueReader["group_id"]);
                    quickLeague.numUp = Convert.ToInt64(quickLeagueReader["num_up"]);
                    quickLeague.numDown = Convert.ToInt64(quickLeagueReader["num_down"]);
                    quickLeague.numGroupMember = Convert.ToInt64(quickLeagueReader["num_in_group"]);
                    quickLeague.numLeagueMember = Convert.ToInt64(quickLeagueReader["num_in_league"]);
                    quickLeague.highScoreOpe = OperatorScore.GenerateEndlessLeagueHighScorePrizes(conn, quickLeague.leagueId);
                    quickLeague.highScoreOpe = OperatorScore.GenerateEndlessLeagueTotalScorePrizes(conn, quickLeague.leagueId);

                    var playerSql = Db.GetCommand(@"SELECT * FROM `sw_player` WHERE id='{0}' ORDER BY quick_ranking_league_group, quick_ranking_league_group_id, quick_total_score DESC", uid);
                    var playerCommand = new MySqlCommand(playerSql, conn);
                    var playerReader = playerCommand.ExecuteReader();

                    if (playerReader.HasRows)
                    {
                        playerState.quickRankingLeagueGroup = Convert.ToInt64(playerReader["quick_ranking_league_group"]);
                        playerState.quickRankingLeague = Convert.ToInt64(playerReader["quick_ranking_league"]);
                        if (playerState.quickRankingLeagueGroup <= quickLeague.numUp)
                        {
                            quickLeague.leagueId += 1;
                        }
                        else if (quickLeague.leagueId >= 9 && playerState.quickRankingLeagueGroup > (quickLeague.numGroupMember - quickLeague.numDown))
                        {
                            quickLeague.leagueId -= 1;
                        }
                        playerState.quickRankingLeague = quickLeague.leagueId;
                    }

                    playerReader.Close();
                }

                quickLeagueReader.Close();
            }

            var updatePlayerStateSql = Db.GetCommand(@"UPDATE `sw_players` SET quick_ranking_league = '{0}' WHERE id= '{1}'", playerState.quickRankingLeague, uid);
            var updatePlayerStateCommand = new MySqlCommand(updatePlayerStateSql, conn);
            updatePlayerStateCommand.ExecuteNonQuery();

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
