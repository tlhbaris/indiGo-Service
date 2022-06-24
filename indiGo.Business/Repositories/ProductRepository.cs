using indiGo.Business.Repositories.Abstract.EntityFrameworkCore;
using indiGo.Core.Entities;
using indiGo.Data.EntityFramework;

namespace indiGo.Business.Repositories;

public class ProductRepository : RepositoryBase<Product,int>
{
    public ProductRepository(MyContext context) : base(context)
    {
    }
}