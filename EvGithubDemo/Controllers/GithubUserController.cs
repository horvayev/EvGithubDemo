using BusinessLogic;
using Entities;
using EvGithubDemoWebApi.Caching;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EvGithubDemoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GithubUserController : ApiControllerBase
    {
        public GithubUserController(IBusinessContext businessContext) : base(businessContext)
        {
        }

        // GET: api/GithubUser/login
        [HttpGet("{login}", Name = "Get")]
        [Cache(10)]
        public async Task<IActionResult> Get(string login)
        {
            GithubUser githubUser = await BusinessContext.GithubUserService.GetGithubUser(login);
            if (githubUser == null)
            {
                return NotFound();
            }

            return Ok(githubUser);
        }
    }
}