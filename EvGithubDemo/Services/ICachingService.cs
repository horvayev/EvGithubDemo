using System;

namespace EvGithubDemoWebApi.Services
{
    public interface ICachingService
    {
        void Put(string key, object response, TimeSpan timeToLive);

        object Get(string key);
    }
}