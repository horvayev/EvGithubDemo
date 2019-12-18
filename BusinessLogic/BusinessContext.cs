using BusinessLogic.Services;
using DataAccess;

namespace BusinessLogic
{
    public class BusinessContext : IBusinessContext
    {
        private IDataContext DataContext { get; set; }
        public IGithubUserService GithubUserService { get; private set; }
        public EmtyService EmptyService { get; private set; }

        public BusinessContext(IDataContext dataContext)
        {
            DataContext = dataContext;
            GithubUserService = new GithubUserService(this, DataContext, new Github.GithubClient());
            EmptyService = new EmtyService(this, DataContext);
        }
    }
}