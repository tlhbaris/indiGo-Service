using System.Linq.Expressions;
using indiGo.Core.Entities.Abstract;

namespace indiGo.Business.Repositories.Abstract;

public interface IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
{
    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null);
    TEntity GetById(TKey id);
    TKey Insert(TEntity entity);
    int Update(TEntity entity);
    int Delete(TEntity entity);
    int Save();
}