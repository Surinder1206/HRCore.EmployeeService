using HRCore.EmployeeService.Application.Interfaces;
using HRCore.EmployeeService.Application.NotificationMessages;
using Moq;

namespace HRCore.EmployeeService.Tests.Builders;

internal class MessageServiceMockBuilder : Builder<Mock<IMessagingService>>
{
    private Mock<IMessagingService> _messageServiceMock = new();

    protected override Mock<IMessagingService> OnBuild()
    {
        _messageServiceMock
            .Setup(m => m.SendCreateEmployeeMessage(It.IsAny<EmployeeMessageBody>()));

        return _messageServiceMock;
    }
}
