using EvGithubDemoWebApi.Caching.Strategy;

namespace EvGithubDemoWebApi.Caching
{
    public class MemoryCache<TItem> where TItem : class
    {
        private CachingStrategy<TItem> _cachingStrategy;

        public MemoryCache(CachingStrategy<TItem> cachingStrategy)
        {
            _cachingStrategy = cachingStrategy;
        }

        public void Put(string key, TItem value)
        {
            _cachingStrategy.Put(key, value);
        }

        public TItem Get(string key)
        {
            return _cachingStrategy.Get(key);
        }
    }
}