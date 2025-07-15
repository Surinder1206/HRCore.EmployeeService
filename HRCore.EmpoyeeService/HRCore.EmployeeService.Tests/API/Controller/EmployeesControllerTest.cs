using HRCore.EmployeeService.Application.DTOs;
using HRCore.EmployeeService.Application.Interfaces;
using Moq;
using NUnit.Framework;

namespace HRCore.EmployeeService.Tests.API.Controller;

internal class EmployeesControllerTest
{
    [Test]
    public async Task CreateEmployeeAsync_should_call_service_when_attempting_to_create_app()
    {
        // Arrange
        var employeeServiceMock = new Mock<IEmployeeAppService>();
        var controller = new EmployeesController(employeeServiceMock.Object);

        // Act
        await controller.CreateEmployeeAsync(new CreateEmployeeRequest());

        // Assert
        employeeServiceMock.Verify(s => s.CreateAsync(It.IsAny<EmployeeDto>()), Times.Once);
    }
}
