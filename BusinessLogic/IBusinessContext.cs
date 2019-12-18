using BusinessLogic.Services;

namespace BusinessLogic
{
    public interface IBusinessContext
    {
        IGithubUserService GithubUserService { get; }
        EmtyService EmptyService { get; }
    }
}