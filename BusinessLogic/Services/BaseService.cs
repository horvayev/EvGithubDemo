using DataAccess;

namespace BusinessLogic.Services
{
    public abstract class BaseService
    {
        protected IBusinessContext BusinessContext { get; private set; }
        protected IDataContext DataContext { get; private set; }

        public BaseService(IBusinessContext bussinessContext, IDataContext dataContext)
        {
            this.BusinessContext = bussinessContext;
            this.DataContext = dataContext;
        }
    }
}