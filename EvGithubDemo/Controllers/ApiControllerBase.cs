using BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace EvGithubDemoWebApi.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {
        protected IBusinessContext BusinessContext { get; private set; }

        public ApiControllerBase(IBusinessContext businessContext)
        {
            BusinessContext = businessContext;
        }
    }
}