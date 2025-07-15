using System.Linq.Expressions;

namespace HRCore.EmployeeService.Domain.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task InsertAsync(TEntity entity);

    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null!, string includeProperties = "", bool IsNoTracking = false);
}
