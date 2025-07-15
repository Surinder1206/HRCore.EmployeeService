using HRCore.EmployeeService.Application.DTOs;
using HRCore.EmployeeService.Application.Interfaces;
using HRCore.EmployeeService.Application.Results;
using HRCore.EmployeeService.Domain.Entities;

namespace HRCore.EmployeeService.Application.Services;

public class EmployeeAppService(IUnitOfWork unitOfWork, IMessagingService messagingService)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMessagingService _messagingService = messagingService;

    public async Task<ServiceResult<EmployeeDto>> CreateAsync(EmployeeDto employeeDto)
    {
        var Id = Guid.NewGuid();

        var existingEmailEmployee = await _unitOfWork.EmployeeRepository.FirstOrDefaultAsync(e => e.Email == employeeDto.Email);
        if (existingEmailEmployee != null)
        {
            return ServiceResult.Fail<EmployeeDto>("An employee with the same email already exists.", ErrorType.BadRequest);
        }
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
        await _messagingService.SendCreateEmployeeMessage(new NotificationMessages.EmployeeMessageBody() { Email = employee.Email, FullName = employee.FullName });

        return ServiceResult.Success(new EmployeeDto()
        {
            Id = Id,
            FullName = employee.FullName,
            Department = employee.Department,
            Email = employee.Email,
            Role = employee.Role,
            Address = employee.Address,
            DateOfJoining = employee.DateOfJoining,
            Status = employee.Status
        });
    }
}
