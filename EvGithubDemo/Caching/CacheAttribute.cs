using EvGithubDemoWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace EvGithubDemoWebApi.Caching
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSeconds;

        public CacheAttribute(int timeToLiveInSeconds)
        {
            _timeToLiveInSeconds = timeToLiveInSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ICachingService cachingService = context.HttpContext.RequestServices.GetRequiredService<ICachingService>();
            string key = context.HttpContext.Request.Path;
            object cachedData = cachingService.Get(key);

            if (cachedData != null)
            {
                ContentResult contentResult = new ContentResult
                {
                    Content = JsonSerializer.Serialize(cachedData),
                    StatusCode = 200,
                    ContentType = "application/json"
                };
                context.Result = contentResult;
                return;
            }

            ActionExecutedContext executedContext = await next();

            if (executedContext.Result is OkObjectResult okObjectResult)
            {
                cachingService.Put(key, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveInSeconds));
            }
        }
    }
}