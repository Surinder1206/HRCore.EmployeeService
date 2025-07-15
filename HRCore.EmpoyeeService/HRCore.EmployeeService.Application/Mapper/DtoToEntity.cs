using HRCore.EmployeeService.Application.DTOs;
using HRCore.EmployeeService.Domain.Entities;

namespace HRCore.EmployeeService.Application.Mapper;

public static class DtoToEntity
{
    public static Employee ToEmployeeEntity(
    this EmployeeDto employeeDto)
    {
        return new Employee
        {
            Id = Guid.NewGuid(),
            FullName = employeeDto.FullName,
            Department = employeeDto.Department,
            Email = employeeDto.Email,
            Role = employeeDto.Role,
            Address = employeeDto.Address,
            DateOfJoining = employeeDto.DateOfJoining,
            Status = employeeDto.Status
        };
    }
}
