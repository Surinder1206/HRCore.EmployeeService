using HRCore.EmployeeService.Domain.Entities;

namespace HRCore.EmployeeService.Tests.Builders;

internal class EmployeeBuilder : Builder<Employee>
{
    private string _email = "dummy@gmail.com";

    public EmployeeBuilder WithEmail(string email)
    {
        this._email = email;
        return this;
    }

    protected override Employee OnBuild() => new()
    {
        Id = Guid.NewGuid(),
        FullName = "Surinder Singh",
        Department = "Engineering",
        Email = _email,
        Role = "Software Engineer",
        Address = "123 Main St, City, Country",
        DateOfJoining = DateTime.UtcNow,
        Status = "Active"
    };
}
