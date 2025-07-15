using System.Linq.Expressions;
using HRCore.EmployeeService.Domain.Interfaces;
using HRCore.EmployeeService.Infrastructure.Persistence.DBContext;
using Microsoft.EntityFrameworkCore;

namespace HRCore.EmployeeService.Infrastructure.Persistence.Repositories;

public class GenericRepository<TEntity>(EmployeeServiceDBContext context) : IGenericRepository<TEntity> where TEntity : class
{
    internal EmployeeServiceDBContext _context = context;
    internal DbSet<TEntity> dbSet = context.Set<TEntity>();

    public virtual async Task<TEntity> FirstOrDefaultAsync(
       Expression<Func<TEntity, bool>> filter = null,
       string includeProperties = "", bool IsNoTracking = false)
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }
        if (IsNoTracking)
        {
            return await query.AsNoTracking<TEntity>().FirstOrDefaultAsync();
        }
        return await query.FirstOrDefaultAsync();
    }

    public virtual async Task InsertAsync(TEntity entity)
    {
        await dbSet.AddAsync(entity);
    }
}
