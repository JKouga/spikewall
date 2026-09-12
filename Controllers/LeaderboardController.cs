using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using spikewall.Debug;
using spikewall.Encryption;
using spikewall.Object;
using spikewall.Request;
using spikewall.Response;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace spikewall.Controllers
{
    [ApiController]
    [Route("Leaderboard")]
    public class LeaderboardController : ControllerBase
    {
        [HttpPost]
        [Route("getWeeklyLeaderboardOptions")]
        [Produces("text/json")]
        public JsonResult GetWeeklyLeaderboardOptions([FromForm] string param, [FromForm] string secure, [FromForm] string key = "")
        {
            var iv = (string)Config.Get("encryption_iv");

            using var conn = Db.Get();
            conn.Open();

            var clientReq = new ClientRequest<LeaderboardRequest>(conn, param, secure, key);
            if (clientReq.error != SRStatusCode.Ok) {
                return new JsonResult(EncryptedResponse.Generate(iv, clientReq.error));
            }

            PlayerState playerState = new();
            var populateStatus = playerState.Populate(conn, clientReq.userId);
            if (populateStatus != SRStatusCode.Ok)
            {
                return new JsonResult(EncryptedResponse.Generate(iv, populateStatus));
            }

            LeaderboardRequest leaderboardRequest = new();

            var rankingLeague = playerState.rankingLeague;
            var rankingLeaguegroup = playerState.rankingLeagueGroup;

            if (leaderboardRequest.Mode == 1)
            {
                rankingLeague = playerState.quickRankingLeague;
                rankingLeaguegroup = playerState.quickRankingLeagueGroup;
            }

            var startResetStatus = LeagueData.GetStartAndEndTimesForEndlessLeague(conn, (long)rankingLeague, (long)rankingLeaguegroup, out long startTime, out long resetTime);
            if (startResetStatus != SRStatusCode.Ok)
            {
                return new JsonResult(EncryptedResponse.Generate(iv, startResetStatus));
            }

            WeeklyLeaderboardOptionsResponse weeklyLeaderboardOptionsResponse = new()
            {
                mode = leaderboardRequest.Mode,
                startTime = startTime,
                resetTime = resetTime
            };

            return new JsonResult(EncryptedResponse.Generate(iv, weeklyLeaderboardOptionsResponse));
        }

        [HttpPost]
        [Route("getWeeklyLeaderboardEntries")]
        [Produces("text/json")]
        public JsonResult GetWeeklyLeaderboardEntries([FromForm] string param, [FromForm] string secure, [FromForm] string key = "")
        {
            var iv = (string)Config.Get("encryption_iv");

            using var conn = Db.Get();
            conn.Open();

            var clientReq = new ClientRequest<BaseRequest>(conn, param, secure, key);
            if (clientReq.error != SRStatusCode.Ok) {
                return new JsonResult(EncryptedResponse.Generate(iv, clientReq.error));
            }

            PlayerState playerState = new();
            var populateStatus = playerState.Populate(conn, clientReq.userId);
            if (populateStatus != SRStatusCode.Ok)
            {
                return new JsonResult(EncryptedResponse.Generate(iv, populateStatus));
            }

            LeaderboardEntriesRequest leaderboardEntriesRequest = new();
            WeeklyLeaderboardEntriesResponse weeklyLeaderboardEntriesResponse = new();

            var rankingLeague = playerState.rankingLeague;
            var rankingLeagueGroup = playerState.rankingLeagueGroup;

            if (leaderboardEntriesRequest.Mode == 1)
            {
                rankingLeague = playerState.quickRankingLeague;
                rankingLeagueGroup = playerState.quickRankingLeagueGroup;
            }

            var endlessStartResetStatus = LeagueData.GetStartAndEndTimesForEndlessLeague(conn, (long)rankingLeague, (long)rankingLeagueGroup, out long endlessStartTime, out long endlessResetTime);
            if (endlessStartResetStatus != SRStatusCode.Ok)
            {
                return new JsonResult(EncryptedResponse.Generate(iv, endlessStartResetStatus));
            }

            var quickStartResetStatus = LeagueData.GetStartAndEndTimesForQuickLeague(conn, (long)rankingLeague, (long)rankingLeagueGroup, out long quickStartTime, out long quickResetTime);
            if (endlessStartResetStatus != SRStatusCode.Ok)
            {
                return new JsonResult(EncryptedResponse.Generate(iv, endlessStartResetStatus));
            }

            if (leaderboardEntriesRequest.Type == 4 || leaderboardEntriesRequest.Type == 5)
            {
                if (leaderboardEntriesRequest.Mode == 1)
                {
                    rankingLeague = playerState.quickRankingLeague;
                    rankingLeagueGroup = playerState.quickRankingLeagueGroup;
                    var quickEntryListStatus = LeagueData.GetQuickLeagueHighScores(conn, clientReq.userId, leaderboardEntriesRequest.Type, (long)rankingLeague, (long)rankingLeagueGroup, out LeaderboardEntry playerEntry, out long quickEntryCount, out LeaderboardEntry[] quickLeaderboardEntries);
                    if (quickEntryListStatus != SRStatusCode.Ok)
                    {
                        return new JsonResult(EncryptedResponse.Generate(iv, quickEntryListStatus));
                    }

                    weeklyLeaderboardEntriesResponse.playerEntry = playerEntry;
                    weeklyLeaderboardEntriesResponse.startTime = quickStartTime;
                    weeklyLeaderboardEntriesResponse.resetTime = quickResetTime;
                    weeklyLeaderboardEntriesResponse.startIndex = leaderboardEntriesRequest.First;
                    weeklyLeaderboardEntriesResponse.mode = leaderboardEntriesRequest.Mode;
                    weeklyLeaderboardEntriesResponse.totalEntries = quickEntryCount;
                    weeklyLeaderboardEntriesResponse.entriesList = quickLeaderboardEntries;
                }
                else
                {
                    rankingLeague = playerState.rankingLeague;
                    rankingLeagueGroup = playerState.rankingLeagueGroup;
                    var endlessEntryListStatus = LeagueData.GetEndlessLeagueHighScores(conn, clientReq.userId, leaderboardEntriesRequest.Type, (long)rankingLeague, (long)rankingLeagueGroup, out LeaderboardEntry playerEntry, out long endlessEntryCount, out LeaderboardEntry[] endlessLeaderboardEntries);

                    if (endlessEntryListStatus != SRStatusCode.Ok)
                    {
                        return new JsonResult(EncryptedResponse.Generate(iv, endlessEntryListStatus));
                    }

                    weeklyLeaderboardEntriesResponse.playerEntry = playerEntry;
                    weeklyLeaderboardEntriesResponse.startTime = quickStartTime;
                    weeklyLeaderboardEntriesResponse.resetTime = quickResetTime;
                    weeklyLeaderboardEntriesResponse.startIndex = leaderboardEntriesRequest.First;
                    weeklyLeaderboardEntriesResponse.mode = leaderboardEntriesRequest.Mode;
                    weeklyLeaderboardEntriesResponse.totalEntries = endlessEntryCount;
                    weeklyLeaderboardEntriesResponse.entriesList = endlessLeaderboardEntries;
                }
            }
            else if (leaderboardEntriesRequest.Type == 6 || leaderboardEntriesRequest.Type == 7)
            {
                if (leaderboardEntriesRequest.Mode == 1 && (DateTimeOffset.Now.ToUnixTimeSeconds() < quickResetTime && DateTimeOffset.Now.ToUnixTimeSeconds() >= quickStartTime))
                {
                    var quickLeaderboardStatus = LeagueData.GetQuickHighScores(conn, clientReq.userId, out LeaderboardEntry playerEntry, out LeaderboardEntry[] quickLeaderboard, out long quickLeaderboardPlayers);
                    if (quickLeaderboardStatus != SRStatusCode.Ok)
                    {
                        return new JsonResult(EncryptedResponse.Generate(iv, quickLeaderboardStatus));
                    }

                    weeklyLeaderboardEntriesResponse.playerEntry = playerEntry;
                    weeklyLeaderboardEntriesResponse.startTime = quickStartTime;
                    weeklyLeaderboardEntriesResponse.resetTime = quickResetTime;
                    weeklyLeaderboardEntriesResponse.startIndex = leaderboardEntriesRequest.First;
                    weeklyLeaderboardEntriesResponse.mode = leaderboardEntriesRequest.Mode;
                    weeklyLeaderboardEntriesResponse.totalEntries = quickLeaderboardPlayers;
                    weeklyLeaderboardEntriesResponse.entriesList = quickLeaderboard;
                }
                else if (DateTimeOffset.Now.ToUnixTimeSeconds() < endlessResetTime && DateTimeOffset.Now.ToUnixTimeSeconds() >= endlessStartTime)
                {
                    var endlessLeaderboardStatus = LeagueData.GetEndlessHighScores(conn, clientReq.userId, out LeaderboardEntry playerEntry, out LeaderboardEntry[] endlessLeaderboard, out long endlessLeaderboardPlayers);
                    if (endlessLeaderboardStatus != SRStatusCode.Ok)
                    {
                        return new JsonResult(EncryptedResponse.Generate(iv, endlessLeaderboardStatus));
                    }

                    weeklyLeaderboardEntriesResponse.playerEntry = playerEntry;
                    weeklyLeaderboardEntriesResponse.startTime = endlessStartTime;
                    weeklyLeaderboardEntriesResponse.resetTime = endlessResetTime;
                    weeklyLeaderboardEntriesResponse.startIndex = leaderboardEntriesRequest.First;
                    weeklyLeaderboardEntriesResponse.mode = leaderboardEntriesRequest.Mode;
                    weeklyLeaderboardEntriesResponse.totalEntries = endlessLeaderboardPlayers;
                    weeklyLeaderboardEntriesResponse.entriesList = endlessLeaderboard;
                }
            }
            return new JsonResult(EncryptedResponse.Generate(iv, weeklyLeaderboardEntriesResponse));
        }

        [HttpPost]
        [Route("getLeagueData")]
        [Produces("text/json")]
        public JsonResult GetLeagueData([FromForm] string param, [FromForm] string secure, [FromForm] string key = "")
        {
            var iv = (string)Config.Get("encryption_iv");

            using var conn = Db.Get();
            conn.Open();

            var clientReq = new ClientRequest<BaseRequest>(conn, param, secure, key);
            if (clientReq.error != SRStatusCode.Ok) {
                return new JsonResult(EncryptedResponse.Generate(iv, clientReq.error));
            }

            PlayerState playerState = new();
            var populateStatus = playerState.Populate(conn, clientReq.userId);
            if (populateStatus != SRStatusCode.Ok)
            {
                return new JsonResult(EncryptedResponse.Generate(iv, populateStatus));
            }

            LeaderboardRequest leaderboardRequest = new();
            LeagueDataResponse leagueDataResponse = new();

            if (leaderboardRequest.Mode == 0)
            {
                LeagueData.GenerateEndlessLeagueData(conn, clientReq.userId, out LeagueData currentEndlessLeague);
                leagueDataResponse.mode = leaderboardRequest.Mode;
                leagueDataResponse.leagueData = currentEndlessLeague;
            }
            else
            {
                LeagueData.GenerateQuickLeagueData(conn, clientReq.userId, out LeagueData currentQuickLeague);
                leagueDataResponse.mode = leaderboardRequest.Mode;
                leagueDataResponse.leagueData = currentQuickLeague;
            }
            return new JsonResult(EncryptedResponse.Generate(iv, leagueDataResponse));
        }

        [HttpPost]
        [Route("getLeagueOperatorData")]
        [Produces("text/json")]
        public JsonResult GetLeagueOperatorData([FromForm] string param, [FromForm] string secure, [FromForm] string key = "")
        {
            var iv = (string)Config.Get("encryption_iv");

            using var conn = Db.Get();
            conn.Open();

            var clientReq = new ClientRequest<BaseRequest>(conn, param, secure, key);
            if (clientReq.error != SRStatusCode.Ok)
            {
                return new JsonResult(EncryptedResponse.Generate(iv, clientReq.error));
            }

            PlayerState playerState = new();
            var populateStatus = playerState.Populate(conn, clientReq.userId);
            if (populateStatus != SRStatusCode.Ok)
            {
                return new JsonResult(EncryptedResponse.Generate(iv, populateStatus));
            }

            LeaderboardRequest leaderboardRequest = new();
            LeagueOperatorDataResponse leagueOperatorDataResponse = new();

            if (leaderboardRequest.Mode == 0)
            {
                LeagueData.GenerateEndlessLeagueDataList(conn, clientReq.userId, out LeagueData[] endlessLeague);
                leagueOperatorDataResponse.LeagueID = Convert.ToInt64(endlessLeague[(int)playerState.rankingLeague]);
                leagueOperatorDataResponse.LeagueList = endlessLeague;
            }
            else
            {
                LeagueData.GenerateQuickLeagueDataList(conn, clientReq.userId, out LeagueData[] quickLeague);
                leagueOperatorDataResponse.LeagueID = Convert.ToInt64(quickLeague[(int)playerState.quickRankingLeague]);
                leagueOperatorDataResponse.LeagueList = quickLeague;
            }

            return new JsonResult(EncryptedResponse.Generate(iv, leagueOperatorDataResponse));
        }

        [HttpPost]
        [Route("calculateAndResetLeagueData")]
        [Produces("text/json")]
        public JsonResult CalculateAndResetLeagueData([FromForm] string param, [FromForm] string secure, [FromForm] string key = "")
        {
            var iv = (string)Config.Get("encryption_iv");

            using var conn = Db.Get();
            conn.Open();

            var clientReq = new ClientRequest<BaseRequest>(conn, param, secure, key);
            if (clientReq.error != SRStatusCode.Ok)
            {
                return new JsonResult(EncryptedResponse.Generate(iv, clientReq.error));
            }

            PlayerState playerState = new();
            var populateStatus = playerState.Populate(conn, clientReq.userId);
            if (populateStatus != SRStatusCode.Ok)
            {
                return new JsonResult(EncryptedResponse.Generate(iv, populateStatus));
            }

            LeaderboardRequest leaderboardRequest = new();
            LeagueDataResponse leagueDataResponse = new();

            var getCalculateEndlessRunnersLeagueStatus = LeagueData.CalculateEndlessRunnersLeague(conn, clientReq.userId);
            if (getCalculateEndlessRunnersLeagueStatus != SRStatusCode.Ok)
            {
                return new JsonResult(EncryptedResponse.Generate(iv, getCalculateEndlessRunnersLeagueStatus));
            }

            var getCalculateQuickRunnersLeagueStatus = LeagueData.CalculateQuickRunnersLeague(conn, clientReq.userId);
            if (getCalculateQuickRunnersLeagueStatus != SRStatusCode.Ok)
            {
                return new JsonResult(EncryptedResponse.Generate(iv, getCalculateQuickRunnersLeagueStatus));
            }

            var getClearScoresStatus = LeagueData.ClearLeagueScoresData(conn);
            if (getClearScoresStatus != SRStatusCode.Ok)
            {
                return new JsonResult(EncryptedResponse.Generate(iv, getClearScoresStatus));
            }

            if (leaderboardRequest.Mode == 0)
            {
                LeagueData.GenerateEndlessLeagueData(conn, clientReq.userId, out LeagueData currentEndlessLeague);
                leagueDataResponse.mode = leaderboardRequest.Mode;
                leagueDataResponse.leagueData = currentEndlessLeague;

                switch (currentEndlessLeague.leagueId)
                {
                    case 3:
                    case 6:
                    case 9:
                    case 12:
                    case 15:
                    case 18:
                    case 19:
                    case 20:
                        var populateChaoState = Chao.PopulateChaoState(conn, clientReq.userId, out Chao[] chaoState);
                        if (populateChaoState != SRStatusCode.Ok)
                        {
                            return new JsonResult(EncryptedResponse.Generate(iv, populateChaoState));
                        }
                        Chao chao = new();
                        chao.chaoID = Convert.ToString(Chao.ChaoID.Shahra);
                        var getChaoIndex = Chao.FindChaoInChaoState(Convert.ToInt32(chao.chaoID), chaoState);
                        if (chaoState[getChaoIndex].status == (sbyte)Chao.Status.NotOwned || chaoState[getChaoIndex].level < 10)
                        {
                            var chaoPrize = ChaoSpinPrize.ChaoToChaoSpinPrize(chao);
                        }
                        else if (chaoState[getChaoIndex].status == (sbyte)Chao.Status.MaxLevel)
                        {
                            var itemPrize = new Item((long)Item.ItemID.SpecialEgg, 1);
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                LeagueData.GenerateQuickLeagueData(conn, clientReq.userId, out LeagueData currentQuickLeague);
                leagueDataResponse.mode = leaderboardRequest.Mode;
                leagueDataResponse.leagueData = currentQuickLeague;

                switch (currentQuickLeague.leagueId)
                {
                    case 3:
                    case 6:
                    case 9:
                    case 12:
                    case 15:
                    case 18:
                    case 19:
                    case 20:
                        var populateChaoState = Chao.PopulateChaoState(conn, clientReq.userId, out Chao[] chaoState);
                        if (populateChaoState != SRStatusCode.Ok)
                        {
                            return new JsonResult(EncryptedResponse.Generate(iv, populateChaoState));
                        }
                        Chao chao = new();
                        chao.chaoID = Convert.ToString(Chao.ChaoID.DarkQueen);
                        var getChaoIndex = Chao.FindChaoInChaoState(Convert.ToInt32(chao.chaoID), chaoState);
                        if (chaoState[getChaoIndex].status == (sbyte)Chao.Status.NotOwned || chaoState[getChaoIndex].level < 10)
                        {
                            var chaoPrize = ChaoSpinPrize.ChaoToChaoSpinPrize(chao);
                        }
                        else if (chaoState[getChaoIndex].status == (sbyte)Chao.Status.MaxLevel)
                        {
                            var itemPrize = new Item((long)Item.ItemID.SpecialEgg, 1);
                        }
                        break;
                    default:
                        break;
                }
            }
            return new JsonResult(EncryptedResponse.Generate(iv, leagueDataResponse));
        }
    }
}
