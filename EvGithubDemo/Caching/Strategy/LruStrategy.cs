using System.Collections.Generic;
using System.Linq;

namespace EvGithubDemoWebApi.Caching.Strategy
{
    public sealed class LruStrategy<TItem> : CachingStrategy<TItem> where TItem : class
    {
        private LinkedList<string> Keys { get; set; }
        private Dictionary<string, TItem> Store { get; set; }

        public LruStrategy(int maxSize) : base(maxSize)
        {
            Keys = new LinkedList<string>();
            Store = new Dictionary<string, TItem>(maxSize);
        }

        public override TItem Get(string key)
        {
            if (!Store.ContainsKey(key))
            {
                return null;
            }

            Keys.Remove(key);
            Keys.AddLast(key);
            return Store[key];
        }

        public override void Put(string key, TItem value)
        {
            if (!Store.ContainsKey(key))
            {
                if (Store.Count == MaxSize)
                {
                    // remove first key (the oldest)
                    string keyToRemove = Keys.First();
                    Keys.RemoveFirst();
                    Store.Remove(keyToRemove);
                }
                Keys.AddLast(key);
                Store.Add(key, value);
            }
            else
            {
                Keys.Remove(key);
                Keys.AddLast(key);
                Store[key] = value;
            }
        }
    }
}