using System;

namespace AngularJSAuthentication.Caching
{
    public interface ICacheProvider
    {
        //void Set<T>(string key, T value);

        //void Set<T>(string key, T value, TimeSpan timeout);

        //T Get<T>(string key);

        //T GetOrSet<T>(string cacheKey, Func<T> getItemCallback);

        //bool Remove(string key);

        bool IsInCache(string key);
    }
}
