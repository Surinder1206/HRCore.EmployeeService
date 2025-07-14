using HRCore.EmployeeService.Application.Interfaces;
using HRCore.EmployeeService.Domain.Entities;
using HRCore.EmployeeService.Domain.Interfaces;

namespace HRCore.EmployeeService.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    public IGenericRepository<Employee> EmployeeRepository => throw new NotImplementedException();

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}
