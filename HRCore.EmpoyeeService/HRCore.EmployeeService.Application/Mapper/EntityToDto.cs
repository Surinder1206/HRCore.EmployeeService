using HRCore.EmployeeService.Application.DTOs;
using HRCore.EmployeeService.Domain.Entities;

namespace HRCore.EmployeeService.Application.Mapper;

public static class EntityToDto
{
    public static EmployeeDto ToEmployeeDto(
        this Employee employee)
    {
        return new EmployeeDto
        {
            Id = employee.Id,
            FullName = employee.FullName,
            Department = employee.Department,
            Email = employee.Email,
            Role = employee.Role,
            Address = employee.Address,
            DateOfJoining = employee.DateOfJoining,
            Status = employee.Status,
        };
    }
}
