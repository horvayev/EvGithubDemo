using DataAccess;

namespace BusinessLogic.Services
{
    public class EmtyService : BaseService
    {
        public EmtyService(BusinessContext bussinessContext, IDataContext dataContext) : base(bussinessContext, dataContext)
        {
        }
    }
}