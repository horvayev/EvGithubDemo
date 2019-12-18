using System;

namespace EvGithubDemoWebApi.Caching
{
    public class CacheItem
    {
        private DateTime _createdAt;
        public TimeSpan _timeToLive;
        public object Data { get; set; }

        public CacheItem(object data, TimeSpan timeToLive)
        {
            _createdAt = DateTime.Now;
            _timeToLive = timeToLive;
            Data = data;
        }

        public bool Invalidated
        {
            get
            {
                return (DateTime.Now - _createdAt) >= _timeToLive;
            }
        }
    }
}