using HRCore.EmployeeService.Application.NotificationMessages;

namespace HRCore.EmployeeService.Application.Interfaces;

public interface IMessagingService
{
    Task SendCreateEmployeeMessage(EmployeeMessageBody employeeMessageBody);
}
