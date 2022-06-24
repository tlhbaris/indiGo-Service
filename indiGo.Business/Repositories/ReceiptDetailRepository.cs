using indiGo.Business.Repositories.Abstract.EntityFrameworkCore;
using indiGo.Core.Entities;
using indiGo.Data.EntityFramework;

namespace indiGo.Business.Repositories;

public class ReceiptDetailRepository : RepositoryBase<ReceiptDetail, int>
{
    public ReceiptDetailRepository(MyContext context) : base(context)
    {
    }
}