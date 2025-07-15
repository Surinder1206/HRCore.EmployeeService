using HRCore.EmployeeService.Application.DTOs;
using HRCore.EmployeeService.Application.Interfaces;
using HRCore.EmployeeService.Application.Mapper;
using HRCore.EmployeeService.Application.Results;

namespace HRCore.EmployeeService.Application.Services;

public class EmployeeAppService(IUnitOfWork unitOfWork, IMessagingService messagingService) : IEmployeeAppService
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

        var employee = employeeDto.ToEmployeeEntity();

        await _unitOfWork.EmployeeRepository.InsertAsync(employee);
        await _unitOfWork.SaveAsync();
        await _messagingService.SendCreateEmployeeMessage(new NotificationMessages.EmployeeMessageBody() { Email = employee.Email, FullName = employee.FullName });

        return ServiceResult.Success(employee.ToEmployeeDto());
    }
}
