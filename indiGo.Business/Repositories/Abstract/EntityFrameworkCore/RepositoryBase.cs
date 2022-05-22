using System.Linq.Expressions;
using indiGo.Core.Entities.Abstract;
using indiGo.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace indiGo.Business.Repositories.Abstract.EntityFrameworkCore;

public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
where TEntity : BaseEntity<TKey>
where TKey : IEquatable<TKey>
{
    protected readonly MyContext _context;
    protected readonly DbSet<TEntity> _table;

    public RepositoryBase(MyContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }


    public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null)
    {
        return predicate == null ? _table : _table.Where(predicate);
    }

    public TEntity GetById(TKey id)
    {
        return _table.Find(id);
    }

    public TKey Insert(TEntity entity)
    {
        _table.Add(entity);
        return entity.Id;
    }

    public int Update(TEntity entity)
    {
        _table.Update(entity);
        return 1;
    }

    public int Delete(TEntity entity)
    {
        _table.Remove(entity);
        return 1;
    }

    public int Save()
    {
        return _context.SaveChanges();
    }
}