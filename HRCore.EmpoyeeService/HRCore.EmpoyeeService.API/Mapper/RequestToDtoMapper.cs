using HRCore.EmployeeService.Application.DTOs;
using HRCore.EmpoyeeService.API.Models.Requests;

namespace HRCore.EmpoyeeService.API.Mapper;

public static class RequestToDtoMapper
{
    public static EmployeeDto ToDto(this CreateEmployeeRequest createEmployeeRequest)
    {
        return new EmployeeDto
        {
            Id = Guid.NewGuid(),
            FullName = createEmployeeRequest.FullName,
            Department = createEmployeeRequest.Department,
            Email = createEmployeeRequest.Email,
            Role = createEmployeeRequest.Role,
            Address = createEmployeeRequest.Address,
            DateOfJoining = createEmployeeRequest.DateOfJoining,
            Status = createEmployeeRequest.Status
        };
    }
}
