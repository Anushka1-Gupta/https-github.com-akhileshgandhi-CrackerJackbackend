using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;
using System;
using System.Configuration;

namespace AngularJSAuthentication.Caching
{
    public class RedisCacheProvider : ICacheProvider
    {
        public RedisCacheProvider()
        {
            serializer = new NewtonsoftSerializer();
            RedisConnection = RedisSingleton.RedisInstance;
            client = new StackExchangeRedisCacheClient(RedisConnection, serializer, "");

        }
        private NewtonsoftSerializer serializer { get; set; }
        private StackExchangeRedisCacheClient client { get; set; }
        private IConnectionMultiplexer RedisConnection { get; set; }

        public T Get<T>(string key)
        {
            return client.Get<T>(key);
        }

        public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback)
        {
            if (IsInCache(cacheKey))
            {
                return client.Get<T>(cacheKey);
            }
            else
            {
                var item = getItemCallback();
                if (item != null)
                    client.Add<T>(cacheKey, item);

                return item;
            }

        }

        public bool IsInCache(string key)
        {
            bool isInCache = false;
            isInCache = client.Exists(key);
            return isInCache;
        }

        public bool Remove(string key)
        {
            bool removed = false;
            if (IsInCache(key))
            {
                removed = client.Remove(key); 
            }
            return removed;
        }

        public void Set<T>(string key, T value)
        {
            if (value != null)
                client.Add<T>(key, value);
        }

        public void Set<T>(string key, T value, TimeSpan timeout)
        {
            if (value != null)
                client.Add<T>(key, value,timeout);
        }
    }

    public sealed class RedisSingleton
    {
        private static readonly Lazy<ConnectionMultiplexer> Lazy = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(ConfigurationManager.AppSettings["RedisHostOrIP"] + ":" + ConfigurationManager.AppSettings["RedisPort"]));
        public static ConnectionMultiplexer RedisInstance
        {
            get
            {
                return Lazy.Value;
            }
        }
    }

}
