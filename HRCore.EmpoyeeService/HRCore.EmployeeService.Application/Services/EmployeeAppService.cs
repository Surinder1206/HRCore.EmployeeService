using HRCore.EmployeeService.Application.DTOs;
using HRCore.EmployeeService.Application.Interfaces;
using HRCore.EmployeeService.Domain.Entities;

namespace HRCore.EmployeeService.Application.Services;

public class EmployeeAppService(IUnitOfWork unitOfWork)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<EmployeeDto> CreateAsync(EmployeeDto employeeDto)
    {
        var employee = new Employee
        {
            FullName = employeeDto.FullName,
            Department = employeeDto.Department,
            Email = employeeDto.Email,
            Role = employeeDto.Role,
            Address = employeeDto.Address,
            DateOfJoining = employeeDto.DateOfJoining,
            Status = employeeDto.Status
        };
        await _unitOfWork.EmployeeRepository.InsertAsync(employee);
        await _unitOfWork.SaveAsync();
        return null;
    }
}
