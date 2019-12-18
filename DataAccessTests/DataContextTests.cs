using DataAccess;
using Entities;
using System.Threading.Tasks;
using Xunit;

namespace DataAccessTests
{
    public class DataContextTests
    {
        [Fact]
        public async Task GetGithubUser_ReturnsSavedUser()
        {
            IDataContext context = new DataContext();
            GithubUser user = new GithubUser
            {
                Id = 1,
                Login = "horvayev"
            };
            await context.SaveGithubUser(user);

            GithubUser user2 = await context.GetGithubUser("horvayev");

            Assert.Same(user, user2);
        }

        [Fact]
        public async Task GetGithubUser_ReturnsNull()
        {
            IDataContext context = new DataContext();
            GithubUser user = new GithubUser
            {
                Id = 1,
                Login = "horvayev"
            };
            await context.SaveGithubUser(user);

            GithubUser user2 = await context.GetGithubUser("login1");

            Assert.Null(user2);
        }

        [Fact]
        public async Task SaveGithubUser_UpdatesUser()
        {
            IDataContext context = new DataContext();
            GithubUser user = new GithubUser
            {
                Id = 1,
                Login = "horvayev"
            };
            await context.SaveGithubUser(user);

            GithubUser updatedUser = new GithubUser
            {
                Id = 1,
                Login = "horvayev",
                Name = "Evzen"
            };

            await context.SaveGithubUser(updatedUser);

            GithubUser user2 = await context.GetGithubUser("horvayev");

            Assert.NotNull(user2);
            Assert.Equal("Evzen", user2.Name);
        }
    }
}