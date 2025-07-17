using HRCore.EmployeeService.Application.DTOs;
using HRCore.EmployeeService.Application.Results;
using HRCore.EmpoyeeService.API.Models.Responses;

namespace HRCore.EmpoyeeService.API.Mapper;

public static class DtoToResponseMapper
{
    public static EmployeeResponse ToResponse(this EmployeeDto employeeDto)
    {
        return new EmployeeResponse
        {
            Id = employeeDto.Id,
            FullName = employeeDto.FullName,
            Department = employeeDto.Department,
            Email = employeeDto.Email,
            Role = employeeDto.Role,
            Address = employeeDto.Address,
            DateOfJoining = employeeDto.DateOfJoining,
            Status = employeeDto.Status
        };
    }

    public static ServiceResult<EmployeeResponse> ToProblemResponse(this ServiceResult<EmployeeDto> serviceResult)
    {
        return ServiceResult.Fail<EmployeeResponse>(serviceResult.ErrorMessage, serviceResult.ErrorType);
    }
}
