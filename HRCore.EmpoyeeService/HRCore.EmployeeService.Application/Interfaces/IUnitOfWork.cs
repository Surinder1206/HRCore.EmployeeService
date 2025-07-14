using HRCore.EmployeeService.Domain.Entities;
using HRCore.EmployeeService.Domain.Interfaces;

namespace HRCore.EmployeeService.Application.Interfaces;

public interface IUnitOfWork
{
    IGenericRepository<Employee> EmployeeRepository { get; }

    Task SaveAsync();
}
