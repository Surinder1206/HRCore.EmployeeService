using HRCore.EmployeeService.Application.DTOs;

namespace HRCore.EmployeeService.Tests.Builders;

internal class EmployeeDtoBuilder : Builder<EmployeeDto>
{
    private Guid _id = new();

    public EmployeeDtoBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    protected override EmployeeDto OnBuild() => new()
    {
        Id = _id,
        FullName = "Surinder Singh",
        Department = "Engineering",
        Email = "abc@gmail.com",
        Role = "Software Engineer",
        Address = "123 Main St, City, Country",
        DateOfJoining = DateTime.UtcNow,
        Status = "Active"
    };
}
