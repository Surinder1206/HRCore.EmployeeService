using HRCore.EmployeeService.Application.DTOs;

namespace HRCore.EmployeeService.Tests.Builders;

internal class EmployeeDtoBuilder : Builder<EmployeeDto>
{
    private string _email = "dummy@gmail.com";

    public EmployeeDtoBuilder WithEmail(string email)
    {
        this._email = email;
        return this;
    }

    protected override EmployeeDto OnBuild() => new()
    {
        FullName = "Surinder Singh",
        Department = "Engineering",
        Email = _email,
        Role = "Software Engineer",
        Address = "123 Main St, City, Country",
        DateOfJoining = DateTime.UtcNow,
        Status = "Active"
    };
}
