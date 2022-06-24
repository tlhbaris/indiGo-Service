using indiGo.Business.Repositories.Abstract.EntityFrameworkCore;
using indiGo.Core.Entities;
using indiGo.Data.EntityFramework;

namespace indiGo.Business.Repositories;

public class AddressRepository: RepositoryBase<Address, int>
{
    public AddressRepository(MyContext context) : base(context)
    {
    }
}