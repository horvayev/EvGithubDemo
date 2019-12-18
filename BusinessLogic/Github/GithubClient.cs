using Entities;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLogic.Github
{
    public class GithubClient : IGithubClient
    {
        private const string API_URL = "https://api.github.com/users";

        public async Task<GithubUser> GetUser(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                return null;
            }

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("evaspnetcoredemo", "1"));
                using (var response = await httpClient.GetAsync($"{API_URL}/{login}"))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }

                    string json = await response.Content.ReadAsStringAsync();
                    GithubUser githubUser = JsonSerializer.Deserialize<GithubUser>(json);
                    return githubUser;
                }
            }
        }
    }
}