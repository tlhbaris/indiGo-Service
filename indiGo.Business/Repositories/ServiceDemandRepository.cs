using indiGo.Business.Repositories.Abstract.EntityFrameworkCore;
using indiGo.Core.Entities;
using indiGo.Data.EntityFramework;

namespace indiGo.Business.Repositories;

public class ServiceDemandRepository : RepositoryBase<ServiceDemand, int>
{
    public ServiceDemandRepository(MyContext context) : base(context)
    {
    }
}