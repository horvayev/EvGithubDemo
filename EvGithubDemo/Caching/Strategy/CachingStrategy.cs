namespace EvGithubDemoWebApi.Caching.Strategy
{
    public abstract class CachingStrategy<TItem> where TItem : class
    {
        public int MaxSize { get; }

        public CachingStrategy(int maxSize)
        {
            this.MaxSize = maxSize;
        }

        public abstract void Put(string key, TItem value);

        public abstract TItem Get(string key);
    }
}