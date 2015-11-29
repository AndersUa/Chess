using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace Chess.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    public class GameController : Controller
    {
        private static MemoryCache requestCache = new MemoryCache("_requestsCache");

        [Auth]
        [HttpPost]
        public void SendGameRequest(int userId, [FromHeader]string Autorization)
        {
            var requestFrom = AuthCache.Get(Autorization);
            var requestTo = AuthCache.Get(userId);
            if (requestTo!=null)
            {
                requestCache.Add($"{requestFrom.ID}:{requestTo.ID}", "sad", new CacheItemPolicy() { AbsoluteExpiration = DateTime.Now.AddMinutes(2) });
            }
        }

    }

    

}
