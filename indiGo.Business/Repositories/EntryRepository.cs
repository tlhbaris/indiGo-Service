using indiGo.Business.Repositories.Abstract.EntityFrameworkCore;
using indiGo.Core.Entities;
using indiGo.Data.EntityFramework;

namespace indiGo.Business.Repositories;

public class EntryRepository : RepositoryBase<Entry,int>
{
    public EntryRepository(MyContext context) : base(context)
    {
    }
}