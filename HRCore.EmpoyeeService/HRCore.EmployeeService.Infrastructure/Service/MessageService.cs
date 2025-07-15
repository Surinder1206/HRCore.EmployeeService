using HRCore.EmployeeService.Application.Interfaces;
using HRCore.EmployeeService.Application.NotificationMessages;

namespace HRCore.EmployeeService.Infrastructure.Service;

public class MessageService : IMessagingService
{
    public Task SendCreateEmployeeMessage(EmployeeMessageBody employeeMessageBody)
    {
        return Task.CompletedTask;
    }
}
