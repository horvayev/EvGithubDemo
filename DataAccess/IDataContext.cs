using Entities;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IDataContext
    {
        Task<GithubUser> GetGithubUser(string login);

        Task SaveGithubUser(GithubUser user);
    }
}