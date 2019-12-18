using Entities;
using System.Threading.Tasks;

namespace BusinessLogic.Github
{
    public interface IGithubClient
    {
        Task<GithubUser> GetUser(string login);
    }
}