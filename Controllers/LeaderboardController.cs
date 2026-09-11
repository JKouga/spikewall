using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using spikewall.Debug;
using spikewall.Encryption;
using spikewall.Object;
using spikewall.Request;
using spikewall.Response;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using static spikewall.Object.LeagueData;

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

            var startResetStatus = GetStartAndEndTimesForEndlessLeague(conn, (long)rankingLeague, (long)rankingLeaguegroup, out long startTime, out long resetTime);
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

            // FIXME: Stub

            return new JsonResult(EncryptedResponse.Generate(iv, new WeeklyLeaderboardEntriesResponse()));
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

            // FIXME: Stub

            return new JsonResult(EncryptedResponse.Generate(iv, new BaseResponse()));
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

            // FIXME: Stub

            return new JsonResult(EncryptedResponse.Generate(iv, new BaseResponse()));
        }
    }
}
