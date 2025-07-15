using HRCore.EmployeeService.Application.Interfaces;
using HRCore.EmpoyeeService.API;

namespace HRCore.EmployeeService.Tests.Builders;

internal class EmployeesControllerBuilder : Builder<EmployeesController>
{
    private IEmployeeAppService _employeeAppServiceMock = new EmployeeAppServiceMockBuilder().Build().Object;

    public EmployeesControllerBuilder Using(IEmployeeAppService employeeAppService)
    {
        _employeeAppServiceMock = employeeAppService;
        return this;
    }

    protected override EmployeesController OnBuild()
    {
        return new EmployeesController(_employeeAppServiceMock);

    }
}
