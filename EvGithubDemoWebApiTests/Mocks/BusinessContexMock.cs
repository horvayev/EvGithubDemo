using BusinessLogic;
using BusinessLogic.Services;

namespace EvGithubDemoWebApiTests.Mocks
{
    internal class BusinessContexMock : IBusinessContext
    {
        public IGithubUserService GithubUserService => GithubUserServiceMock.Create().Object;

        public EmtyService EmptyService => throw new System.NotImplementedException();
    }
}