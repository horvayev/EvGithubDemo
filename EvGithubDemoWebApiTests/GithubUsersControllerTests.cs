using BusinessLogic;
using Entities;
using EvGithubDemoWebApi.Controllers;
using EvGithubDemoWebApiTests.Mocks;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace EvGithubDemoWebApiTests
{
    public class GithubUsersControllerTests
    {
        private IBusinessContext _businessContexMock;

        public GithubUsersControllerTests()
        {
            _businessContexMock = new BusinessContexMock();
        }

        [Fact]
        public async Task Get_ReturnsUser()
        {
            GithubUserController ctrl = new GithubUserController(_businessContexMock);
            IActionResult result = await ctrl.Get("horvayev");
            Assert.IsAssignableFrom<OkObjectResult>(result);
            OkObjectResult okResult = result as OkObjectResult;
            Assert.IsType<GithubUser>(okResult.Value);
            GithubUser user = okResult.Value as GithubUser;
            Assert.Equal("horvayev", user.Login);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenUserDoesNotExist()
        {
            GithubUserController ctrl = new GithubUserController(_businessContexMock);
            IActionResult result = await ctrl.Get("non_existent");
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }
    }
}