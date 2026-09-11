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

            var startResetStatus = LeagueData.GetStartAndEndTimesForEndlessLeague(conn, (long)rankingLeague, (long)rankingLeagueGroup, out long startTime, out long resetTime);
            if (startResetStatus != SRStatusCode.Ok)
            {
                return new JsonResult(EncryptedResponse.Generate(iv, startResetStatus));
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
                    weeklyLeaderboardEntriesResponse.startTime = startTime;
                    weeklyLeaderboardEntriesResponse.resetTime = resetTime;
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
                    weeklyLeaderboardEntriesResponse.startTime = startTime;
                    weeklyLeaderboardEntriesResponse.resetTime = resetTime;
                    weeklyLeaderboardEntriesResponse.startIndex = leaderboardEntriesRequest.First;
                    weeklyLeaderboardEntriesResponse.mode = leaderboardEntriesRequest.Mode;
                    weeklyLeaderboardEntriesResponse.totalEntries = endlessEntryCount;
                    weeklyLeaderboardEntriesResponse.entriesList = endlessLeaderboardEntries;
                }
            }
            else if (leaderboardEntriesRequest.Type == 6 || leaderboardEntriesRequest.Type == 7 || DateTimeOffset.Now.ToUnixTimeSeconds() < resetTime && DateTimeOffset.Now.ToUnixTimeSeconds() >= startTime)
            {
                if (leaderboardEntriesRequest.Mode == 1)
                {
                    var quickLeaderboardStatus = LeagueData.GetQuickHighScores(conn, clientReq.userId, out LeaderboardEntry playerEntry, out LeaderboardEntry[] quickLeaderboard, out long quickLeaderboardPlayers);
                    if (quickLeaderboardStatus != SRStatusCode.Ok)
                    {
                        return new JsonResult(EncryptedResponse.Generate(iv, quickLeaderboardStatus));
                    }

                    weeklyLeaderboardEntriesResponse.playerEntry = playerEntry;
                    weeklyLeaderboardEntriesResponse.startTime = startTime;
                    weeklyLeaderboardEntriesResponse.resetTime = resetTime;
                    weeklyLeaderboardEntriesResponse.startIndex = leaderboardEntriesRequest.First;
                    weeklyLeaderboardEntriesResponse.mode = leaderboardEntriesRequest.Mode;
                    weeklyLeaderboardEntriesResponse.totalEntries = quickLeaderboardPlayers;
                    weeklyLeaderboardEntriesResponse.entriesList = quickLeaderboard;
                }
                else
                {
                    var endlessLeaderboardStatus = LeagueData.GetEndlessHighScores(conn, clientReq.userId, out LeaderboardEntry playerEntry, out LeaderboardEntry[] endlessLeaderboard, out long endlessLeaderboardPlayers);
                    if (endlessLeaderboardStatus != SRStatusCode.Ok)
                    {
                        return new JsonResult(EncryptedResponse.Generate(iv, endlessLeaderboardStatus));
                    }

                    weeklyLeaderboardEntriesResponse.playerEntry = playerEntry;
                    weeklyLeaderboardEntriesResponse.startTime = startTime;
                    weeklyLeaderboardEntriesResponse.resetTime = resetTime;
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
        [Route("calculateAndResetLeague")]
        [Produces("text/json")]
        public JsonResult CalculateAndResetLeague([FromForm] string param, [FromForm] string secure, [FromForm] string key = "")
        {
            var iv = (string)Config.Get("encryption_iv");

            using var conn = Db.Get();
            conn.Open();

            var clientReq = new ClientRequest<BaseRequest>(conn, param, secure, key);
            if (clientReq.error != SRStatusCode.Ok)
            {
                return new JsonResult(EncryptedResponse.Generate(iv, clientReq.error));
            }

            // FIXME: Stub

            return new JsonResult(EncryptedResponse.Generate(iv, new BaseResponse()));
        }
    }
}
