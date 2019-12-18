using BusinessLogic.Github;
using DataAccess;
using Entities;
using System;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class GithubUserService : BaseService, IGithubUserService
    {
        private readonly IGithubClient _githubClient;

        public GithubUserService(IBusinessContext bussinessContext, IDataContext dataContext, IGithubClient githubClient) : base(bussinessContext, dataContext)
        {
            _githubClient = githubClient;
        }

        public async Task<GithubUser> GetGithubUser(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new ArgumentException($"Argument {nameof(login)} can not be null or empty");
            }
            // look for a user in local store first
            GithubUser githubUser = await DataContext.GetGithubUser(login);
            if (githubUser != null)
            {
                return githubUser;
            }

            githubUser = await _githubClient.GetUser(login);
            if (githubUser != null)
            {
                await DataContext.SaveGithubUser(githubUser);
            }

            return githubUser;
        }
    }
}