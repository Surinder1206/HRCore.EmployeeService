using HRCore.EmployeeService.Application.Interfaces;
using HRCore.EmployeeService.Application.Services;
using Moq;

namespace HRCore.EmployeeService.Tests.Builders;

internal class EmployeeAppServiceBuilder : Builder<EmployeeAppService>
{
    private IMessagingService _messagingService = new Mock<IMessagingService>().Object;
    private IUnitOfWork _unitOfWork = new UnitOfWorkMockBuilder().Build().Object;

    public EmployeeAppServiceBuilder WithUnitOfWork(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
        return this;
    }

    public EmployeeAppServiceBuilder WithMessagingService(IMessagingService messagingService)
    {
        this._messagingService = messagingService;
        return this;
    }

    protected override EmployeeAppService OnBuild()
    {
        return new EmployeeAppService(_unitOfWork, _messagingService);
    }
}
