﻿using Microsoft.AspNetCore.Mvc;
using spikewall.Debug;
using spikewall.Encryption;
using spikewall.Request;
using spikewall.Response;

namespace spikewall.Controllers
{
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost]
        [Route("/Message/getMessageList/")]
        [Produces("text/json")]
        public JsonResult GetMessageList([FromForm] string param, [FromForm] string secure, [FromForm] string key = "")
        {
            var iv = (string)Config.Get("encryption_iv");

            using var conn = Db.Get();
            conn.Open();

            var clientReq = new ClientRequest<BaseRequest>(conn, param, secure, key);
            if (clientReq.error != SRStatusCode.Ok) {
                return new JsonResult(EncryptedResponse.Generate(iv, clientReq.error));
            }

            // FIXME: Stub

            return new JsonResult(EncryptedResponse.Generate(iv, new MessageListResponse()));
        }
    }
}
