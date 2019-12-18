using BusinessLogic.Services;
using Entities;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvGithubDemoWebApiTests.Mocks
{
    internal class GithubUserServiceMock
    {
        private static List<GithubUser> users = new List<GithubUser>
        {
            new GithubUser
            {
                Id = 1,
                Login = "horvayev"
            },
            new GithubUser
            {
                Id = 2,
                Login = "vuetifyjs"
            }
        };

        internal static Mock<IGithubUserService> Create()
        {
            Mock<IGithubUserService> mock = new Mock<IGithubUserService>();
            mock.Setup(m => m.GetGithubUser(It.IsAny<string>())).Returns((string login) => Task.FromResult(users.FirstOrDefault(x => x.Login == login)));
            return mock;
        }
    }
}