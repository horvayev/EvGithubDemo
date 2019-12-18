using Entities;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public interface IGithubUserService
    {
        Task<GithubUser> GetGithubUser(string login);
    }
}