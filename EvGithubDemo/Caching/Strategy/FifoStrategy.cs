using System.Collections.Generic;
using System.Linq;

namespace EvGithubDemoWebApi.Caching.Strategy
{
    public sealed class FifoStrategy<TItem> : CachingStrategy<TItem> where TItem : class
    {
        private LinkedList<string> Keys { get; set; }
        private Dictionary<string, TItem> Store { get; set; }

        public FifoStrategy(int maxSize) : base(maxSize)
        {
            Keys = new LinkedList<string>();
            Store = new Dictionary<string, TItem>(maxSize);
        }

        public override TItem Get(string key)
        {
            return Store.GetValueOrDefault(key);
        }

        public override void Put(string key, TItem value)
        {
            if (!Store.ContainsKey(key))
            {
                if (Store.Count < MaxSize)
                {
                    Store.Add(key, value);
                    Keys.AddLast(key);
                }
                else
                {
                    string firstKey = Keys.First();
                    Keys.RemoveFirst();
                    Store.Remove(firstKey);
                }
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