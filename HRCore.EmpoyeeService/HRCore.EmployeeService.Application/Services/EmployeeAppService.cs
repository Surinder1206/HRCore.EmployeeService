using HRCore.EmployeeService.Application.DTOs;
using HRCore.EmployeeService.Application.Interfaces;
using HRCore.EmployeeService.Domain.Entities;

namespace HRCore.EmployeeService.Application.Services;

public class EmployeeAppService(IUnitOfWork unitOfWork, IMessagingService messagingService)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMessagingService _messagingService = messagingService;

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
        await _messagingService.SendCreateEmployeeMessage(new NotificationMessages.EmployeeMessageBody() { Email = employee.Email, FullName = employee.FullName });
        return null;
    }
}
