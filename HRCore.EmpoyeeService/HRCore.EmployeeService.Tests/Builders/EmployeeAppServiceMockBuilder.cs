using HRCore.EmployeeService.Application.DTOs;
using HRCore.EmployeeService.Application.Interfaces;
using HRCore.EmployeeService.Application.Results;
using Moq;

namespace HRCore.EmployeeService.Tests.Builders
{
    internal class EmployeeAppServiceMockBuilder : Builder<Mock<IEmployeeAppService>>
    {
        private readonly Mock<IEmployeeAppService> _mock = new();
        private ServiceResult<EmployeeDto> _employeeDto = ServiceResult.Success(new EmployeeDtoBuilder().Build());

        public EmployeeAppServiceMockBuilder ReturnsForCreateEmployee(ServiceResult<EmployeeDto> employeeDto)
        {
            _employeeDto = employeeDto;
            return this;
        }

        protected override Mock<IEmployeeAppService> OnBuild()
        {
            _mock
                .Setup(s => s.CreateAsync(It.IsAny<EmployeeDto>()))
                .ReturnsAsync(_employeeDto);

            return _mock;
        }
    }
}
