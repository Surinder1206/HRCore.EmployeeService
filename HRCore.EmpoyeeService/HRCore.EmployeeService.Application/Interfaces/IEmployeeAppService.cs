using HRCore.EmployeeService.Application.DTOs;
using HRCore.EmployeeService.Application.Results;

namespace HRCore.EmployeeService.Application.Interfaces;

public interface IEmployeeAppService
{
    public Task<ServiceResult<EmployeeDto>> CreateAsync(EmployeeDto employeeDto);

    public Task<ServiceResult<EmployeeDto>> GetEmployeeByIdAsync(Guid id);

    public Task<ServiceResult<List<EmployeeDto>>> GetAllEmployeesAsync();

    public Task<ServiceResult> UpdateEmployeeAsync(Guid id, EmployeeDto employeeDto);
}
