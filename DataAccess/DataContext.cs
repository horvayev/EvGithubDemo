using Entities;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Simple data context implementation for demo purposes.
    /// </summary>
    public class DataContext : IDataContext
    {
        private static ConcurrentDictionary<string, GithubUser> Store { get; set; } = new ConcurrentDictionary<string, GithubUser>();

        public async Task<GithubUser> GetGithubUser(string login)
        {
            await Task.Delay(100);
            return Store.GetValueOrDefault(login);
        }

        public async Task SaveGithubUser(GithubUser user)
        {
            await Task.Delay(100);
            Store.AddOrUpdate(user.Login, user, (oldKey, oldUser) => user);
        }
    }
}