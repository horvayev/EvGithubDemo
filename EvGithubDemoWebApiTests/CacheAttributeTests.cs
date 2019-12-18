using EvGithubDemoWebApi.Caching;
using EvGithubDemoWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EvGithubDemoWebApiTests
{
    public class CacheAttributeTests
    {
        private DefaultHttpContext _defaultHttpContext;
        private Mock<ICachingService> _cachingServiceMock;
        private Mock<IServiceProvider> _serviceProviderMock;
        private ActionContext _actionContext;
        private ActionExecutedContext _context;
        private ActionExecutingContext _actionExecutingContext;

        public CacheAttributeTests()
        {
            _cachingServiceMock = new Mock<ICachingService>();
            _cachingServiceMock.Setup(x => x.Put(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<TimeSpan>()));
            _serviceProviderMock = new Mock<IServiceProvider>();
            _serviceProviderMock.Setup(_ => _.GetService(typeof(ICachingService))).Returns(_cachingServiceMock.Object);

            _defaultHttpContext = new DefaultHttpContext()
            {
                RequestServices = _serviceProviderMock.Object,
            };
            _defaultHttpContext.Request.Path = "/api/githubuser/horvayev";

            ModelStateDictionary modelState = new ModelStateDictionary();

            _defaultHttpContext.Request.Query = new QueryCollection(new Dictionary<string, StringValues> { });

            _actionContext = new ActionContext(
                _defaultHttpContext,
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>(),
                modelState
            );

            _actionExecutingContext = new ActionExecutingContext(
                _actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                Mock.Of<Controller>()
            )
            {
                Result = new OkResult()
            };

            _context = new ActionExecutedContext(_actionContext, new List<IFilterMetadata>(), Mock.Of<Controller>())
            {
                Result = new OkObjectResult("{ id: 1, login: 'horvayev' }")
            };
        }

        [Fact]
        public async Task OnActionExecutionAsync_GetCachedValue()
        {
            CacheAttribute cacheAttribute = new CacheAttribute(10);
            _cachingServiceMock.Setup(x => x.Get(It.IsAny<string>())).Returns((string x) => x);

            await cacheAttribute.OnActionExecutionAsync(_actionExecutingContext, () => Task.FromResult(_context));

            _cachingServiceMock.Verify(x => x.Get(It.IsAny<string>()), Times.Once());
            _cachingServiceMock.Verify(x => x.Put(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<TimeSpan>()), Times.Never());
        }

        [Fact]
        public async Task OnActionExecutionAsync_CacheValue()
        {
            CacheAttribute cacheAttribute = new CacheAttribute(10);
            _cachingServiceMock.Setup(x => x.Get(It.IsAny<string>())).Returns(null);
            await cacheAttribute.OnActionExecutionAsync(_actionExecutingContext, () => Task.FromResult(_context));

            _cachingServiceMock.Verify(x => x.Get(It.IsAny<string>()), Times.Once());
            _cachingServiceMock.Verify(x => x.Put(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<TimeSpan>()), Times.Once());
        }
    }
}