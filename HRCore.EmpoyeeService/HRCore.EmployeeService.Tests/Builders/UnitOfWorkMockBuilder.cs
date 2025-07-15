using HRCore.EmployeeService.Application.Interfaces;
using HRCore.EmployeeService.Domain.Entities;
using HRCore.EmployeeService.Domain.Interfaces;
using Moq;

namespace HRCore.EmployeeService.Tests.Builders;

internal class UnitOfWorkMockBuilder : Builder<Mock<IUnitOfWork>>
{
    private readonly Mock<IUnitOfWork> _mock = new();
    private IGenericRepository<Employee> _employeeRepository = new EmployeeRepositoryMockBuilder().Build().Object;

    public UnitOfWorkMockBuilder Using(IGenericRepository<Employee> repository)
    {
        _employeeRepository = repository;
        return this;
    }

    protected override Mock<IUnitOfWork> OnBuild()
    {
        _mock.Setup(u => u.EmployeeRepository).Returns(_employeeRepository);

        return _mock;
    }
}

