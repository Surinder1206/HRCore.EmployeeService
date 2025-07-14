namespace HRCore.EmployeeService.Domain.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task InsertAsync(TEntity entity);
}
