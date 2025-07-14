namespace HRCore.EmployeeService.Application.NotificationMessages;

public class EmployeeMessageBody
{
    public required string FullName { get; init; }

    public required string Email { get; init; }
}
