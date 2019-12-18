using EvGithubDemoWebApi.Caching;
using EvGithubDemoWebApi.Caching.Strategy;
using System;
using Xunit;

namespace EvGithubDemoWebApiTests
{
    public class LruStrategyTests
    {
        [Fact]
        public void Put_RemovesOldestElement()
        {
            LruStrategy<CacheItem> strategy = new LruStrategy<CacheItem>(2);
            strategy.Put("1", new CacheItem("1", TimeSpan.FromSeconds(10)));
            strategy.Put("2", new CacheItem("2", TimeSpan.FromSeconds(10)));
            strategy.Put("3", new CacheItem("3", TimeSpan.FromSeconds(10)));

            // 1 should be gone
            CacheItem item1 = strategy.Get("1");
            CacheItem item2 = strategy.Get("2");
            CacheItem item3 = strategy.Get("3");

            Assert.Null(item1);
            Assert.NotNull(item2);
            Assert.NotNull(item3);
        }

        [Fact]
        public void Put_RemovesOldestElementAfterGet()
        {
            LruStrategy<CacheItem> strategy = new LruStrategy<CacheItem>(2);
            strategy.Put("1", new CacheItem("1", TimeSpan.FromSeconds(10)));
            strategy.Put("2", new CacheItem("2", TimeSpan.FromSeconds(10)));
            strategy.Get("1");
            // 2 should be the oldest now

            strategy.Put("3", new CacheItem("3", TimeSpan.FromSeconds(10)));

            // 2 should be gone
            CacheItem item1 = strategy.Get("1");
            CacheItem item2 = strategy.Get("2");
            CacheItem item3 = strategy.Get("3");

            Assert.Null(item2);
            Assert.NotNull(item1);
            Assert.NotNull(item3);
        }

        [Fact]
        public void Get_ItemReturned()
        {
            LruStrategy<CacheItem> strategy = new LruStrategy<CacheItem>(2);
            strategy.Put("1", new CacheItem("1", TimeSpan.FromSeconds(10)));

            CacheItem item1 = strategy.Get("1");

            Assert.NotNull(item1);
        }

        [Fact]
        public void Get_NullReturnedWhenItemDoesNotExist()
        {
            LruStrategy<CacheItem> strategy = new LruStrategy<CacheItem>(2);
            CacheItem item1 = strategy.Get("1");
            Assert.Null(item1);
        }
    }
}