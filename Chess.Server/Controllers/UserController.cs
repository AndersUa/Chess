using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.Internal;

namespace Chess.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private static uint index = 0;

        [HttpPost]
        public string Login([FromBody]Credentials creds)
        {
            return AuthCache.Add(new User(index++, creds.Login));
        }

        [Auth]
        [HttpGet]
        public void KeepAlive([FromHeader]string Authorization)
        {
            AuthCache.KeepAlive(Authorization);
        }

        [Auth]
        [HttpGet]
        public User GetUserInfo([FromHeader]string Authorization)
        {
            return AuthCache.Get(Authorization);
        }

        [Auth]
        [HttpGet]
        public User GetUserInfoById(int userId)
        {
            return AuthCache.Get(userId);
        }

        [Auth]
        [HttpGet]
        public User[] GetOnlineUsers()
        {
            return AuthCache.GetUsers();
        }

        [Auth]
        [HttpGet]
        public void LogOut([FromHeader]string Authorization)
        {
            AuthCache.Remove(Authorization);
        }
    }


    public class Credentials
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
