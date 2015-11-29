using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace Chess.Server
{
    public static class AuthCache
    {
        private const int TokenValidityTime = 1;

        private static readonly MemoryCache cache = new MemoryCache("_authCache");

        public static bool ChackAuthToken(string token)
        {
            return string.IsNullOrEmpty(token) ? false : AuthCache.cache.Contains(token);
        }

        public static bool KeepAlive(string token)
        {
            var item = AuthCache.cache.GetCacheItem(token);
            if (item != null)
            {
                AuthCache.cache.Set(item, new CacheItemPolicy() { AbsoluteExpiration = DateTime.Now.AddMinutes(TokenValidityTime) });
                return true;
            }
            return false;
        }

        public static void Remove(string token)
        {
            AuthCache.cache.Remove(token);
        }

        public static string Add(User user)
        {
            var guid = Guid.NewGuid().ToString();
            AuthCache.cache.Add(guid, user, new CacheItemPolicy() { AbsoluteExpiration = DateTime.Now.AddMinutes(TokenValidityTime) });
            return guid;
        }

        public static User Get(string token)
        {
            return (User)AuthCache.cache[token];
        }

        public static User Get(HttpRequest request)
        {
            return (User)AuthCache.cache[request.Headers["Authorization"]];
        }

        public static User Get(int userId)
        {
            return (User)AuthCache.cache.FirstOrDefault(t => t.Value is User && ((User)t.Value).ID == userId).Value;
        }

        public static User[] GetUsers()
        {
            return AuthCache.cache.Select(t => t.Value as User).ToArray();
        }

    }
}
