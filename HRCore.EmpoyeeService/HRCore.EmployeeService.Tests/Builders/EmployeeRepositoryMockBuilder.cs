using System.Linq.Expressions;
using HRCore.EmployeeService.Domain.Entities;
using HRCore.EmployeeService.Domain.Interfaces;
using Moq;

namespace HRCore.EmployeeService.Tests.Builders;

internal class EmployeeRepositoryMockBuilder : Builder<Mock<IGenericRepository<Employee>>>
{
    private readonly Mock<IGenericRepository<Employee>> _mock = new();
    private Employee _employee = new EmployeeBuilder().Build();

    public EmployeeRepositoryMockBuilder ReturnsForFirstOrDefaultAsync(Employee employee)
    {
        _employee = employee;
        return this;
    }

    protected override Mock<IGenericRepository<Employee>> OnBuild()
    {
        _mock
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Employee, bool>>>(), It.IsAny<string>(), It.IsAny<bool>()))
            .ReturnsAsync(_employee);

        _mock
            .Setup(x => x.InsertAsync(It.IsAny<Employee>()))
            .Returns(Task.CompletedTask);

        return _mock;
    }
}
