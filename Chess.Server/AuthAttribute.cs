using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Chess.Server
{
    public class AuthAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(AuthorizationContext context)
        {
            var token = context.HttpContext?.Request?.Headers?["Authorization"];
            if (!AuthCache.ChackAuthToken(token))
                throw new AuthenticationException();
            else
                AuthCache.KeepAlive(token);
        }

        public override Task OnAuthorizationAsync(AuthorizationContext context)
        {
            return Task.Run(() =>
            {
                var token = context.HttpContext?.Request?.Headers?["Authorization"];
                if (!AuthCache.ChackAuthToken(token))
                    throw new AuthenticationException();
                else
                    AuthCache.KeepAlive(token);
            });
        }
    }
}
