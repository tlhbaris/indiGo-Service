using indiGo.Business.Repositories.Abstract.EntityFrameworkCore;
using indiGo.Core.Entities;
using indiGo.Data.EntityFramework;

namespace indiGo.Business.Repositories;

public class ReceiptRepository : RepositoryBase<Receipt,int>
{
    public ReceiptRepository(MyContext context) : base(context)
    {
    }
}