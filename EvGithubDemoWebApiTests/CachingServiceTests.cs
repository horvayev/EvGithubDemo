using EvGithubDemoWebApi.Services;
using System;
using Xunit;

namespace EvGithubDemoWebApiTests
{
    public class CachingServiceTests
    {
        [Fact]
        public void Put_ResponseCached()
        {
            CachingService cachingService = new CachingService();
            cachingService.Put("abc", "response", TimeSpan.FromSeconds(10));
            string response = cachingService.Get("abc") as string;
            Assert.Equal("response", response);
        }

        [Fact]
        public void Put_NullResponseNotCached()
        {
            CachingService cachingService = new CachingService();
            cachingService.Put("abc", null, TimeSpan.FromSeconds(10));
            string response = cachingService.Get("abc") as string;
            Assert.Null(response);
        }

        [Fact]
        public void Get_ReturnsItem()
        {
            CachingService cachingService = new CachingService();
            cachingService.Put("abc", "response", TimeSpan.FromSeconds(10));
            string response = cachingService.Get("abc") as string;
            Assert.Equal("response", response);
        }

        [Fact]
        public void Get_ReturnsNullWhenItemInvalidated()
        {
            CachingService cachingService = new CachingService();
            cachingService.Put("abc", "response", TimeSpan.FromSeconds(0));
            string response = cachingService.Get("abc") as string;
            Assert.Null(response);
        }

        [Fact]
        public void Get_ReturnsNullWhenItemNotCached()
        {
            CachingService cachingService = new CachingService();
            string response = cachingService.Get("abc") as string;
            Assert.Null(response);
        }
    }
}