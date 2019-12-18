using EvGithubDemoWebApi.Caching;
using EvGithubDemoWebApi.Caching.Strategy;
using System;

namespace EvGithubDemoWebApi.Services
{
    public class CachingService : ICachingService
    {
        private readonly MemoryCache<CacheItem> _memoryCache;

        public CachingService()
        {
            _memoryCache = new MemoryCache<CacheItem>(new LruStrategy<CacheItem>(5));
        }

        public void Put(string key, object response, TimeSpan timeToLive)
        {
            if (response == null)
            {
                return;
            }

            CacheItem cacheItem = new CacheItem(response, timeToLive);
            _memoryCache.Put(key, cacheItem);
        }

        public object Get(string key)
        {
            CacheItem cacheItem = _memoryCache.Get(key);
            if (cacheItem != null && cacheItem.Invalidated)
            {
                return null;
            }
            return cacheItem?.Data;
        }
    }
}