using BusinessLogic.Github;
using BusinessLogic.Services;
using DataAccess;
using Entities;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BusinessLogicTests
{
    public class GithubUserServiceTests
    {
        [Fact]
        public async Task GetGithubUser_ThrowsOnNullOrEmptyArgument()
        {
            Mock<IDataContext> dataContextMock = new Mock<IDataContext>();
            IGithubUserService service = new GithubUserService(null, dataContextMock.Object, null);

            await Assert.ThrowsAsync<ArgumentException>(async () => await service.GetGithubUser(null));
        }

        [Fact]
        public async Task GetGithubUser_GetFromLocalStore()
        {
            Mock<IDataContext> dataContextMock = new Mock<IDataContext>();
            dataContextMock.Setup(x => x.GetGithubUser(It.IsAny<string>())).Returns(Task.FromResult(new GithubUser { Id = 1, Login = "horvayev" }));
            dataContextMock.Setup(x => x.SaveGithubUser(It.IsAny<GithubUser>()));

            Mock<IGithubClient> githubClientMock = new Mock<IGithubClient>();
            githubClientMock.Setup(x => x.GetUser(It.IsAny<string>())).Returns(Task.FromResult(new GithubUser { Id = 1, Login = "horvayev" }));

            IGithubUserService service = new GithubUserService(null, dataContextMock.Object, githubClientMock.Object);

            GithubUser user = await service.GetGithubUser("horvayev");

            dataContextMock.Verify(x => x.GetGithubUser(It.IsAny<string>()), Times.Once());
            dataContextMock.Verify(x => x.SaveGithubUser(It.IsAny<GithubUser>()), Times.Never());
        }

        [Fact]
        public async Task GetGithubUser_GetFromGithub()
        {
            Mock<IDataContext> dataContextMock = new Mock<IDataContext>();
            dataContextMock.Setup(x => x.GetGithubUser(It.IsAny<string>())).Returns(Task.FromResult<GithubUser>(null));
            dataContextMock.Setup(x => x.SaveGithubUser(It.IsAny<GithubUser>()));

            Mock<IGithubClient> githubClientMock = new Mock<IGithubClient>();
            githubClientMock.Setup(x => x.GetUser(It.IsAny<string>())).Returns(Task.FromResult(new GithubUser { Id = 1, Login = "horvayev" }));

            IGithubUserService service = new GithubUserService(null, dataContextMock.Object, githubClientMock.Object);

            GithubUser user = await service.GetGithubUser("horvayev");

            dataContextMock.Verify(x => x.GetGithubUser(It.IsAny<string>()), Times.Once());
            dataContextMock.Verify(x => x.SaveGithubUser(It.IsAny<GithubUser>()), Times.Once());
            githubClientMock.Verify(x => x.GetUser(It.IsAny<string>()), Times.Once());
        }
    }
}