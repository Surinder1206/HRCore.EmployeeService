using HRCore.EmployeeService.Application.Interfaces;
using HRCore.EmployeeService.Domain.Entities;
using HRCore.EmployeeService.Domain.Interfaces;
using HRCore.EmployeeService.Infrastructure.Persistence.DBContext;
using HRCore.EmployeeService.Infrastructure.Persistence.Repositories;

namespace HRCore.EmployeeService.Infrastructure.Persistence.UnitOfWork;

public class UnitOfWork(EmployeeServiceDBContext context) : IUnitOfWork
{
    internal EmployeeServiceDBContext _context = context;
    private IGenericRepository<Employee>? _employeeRepository;

    public IGenericRepository<Employee> EmployeeRepository =>
        _employeeRepository ??= new GenericRepository<Employee>(context);


    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
