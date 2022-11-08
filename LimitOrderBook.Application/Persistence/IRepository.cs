using System.Linq.Expressions;

namespace LimitOrderBook.Application.Persistence;

public interface IRepository<TEntity> where TEntity : class
{
    /*
    Task<List<TEntity>> GetAllAsync();

    Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity> Add(TEntity entity);

    Task<TEntity> Remove(TEntity entity); */
}
