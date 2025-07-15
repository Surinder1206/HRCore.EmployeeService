using FluentAssertions;
using HRCore.EmployeeService.Application.DTOs;
using HRCore.EmployeeService.Application.Results;
using HRCore.EmployeeService.Tests.Builders;
using HRCore.EmpoyeeService.API.Mapper;
using HRCore.EmpoyeeService.API.Models.Requests;
using HRCore.EmpoyeeService.API.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace HRCore.EmployeeService.Tests.API.Controller;

internal class EmployeesControllerTest
{
    [Test]
    public async Task CreateEmployeeAsync_should_call_service_when_attempting_to_create_app()
    {
        // Arrange

        var employeeServiceMock = new EmployeeAppServiceMockBuilder()
            .Build();

        var controller = new EmployeesControllerBuilder()
            .Using(employeeServiceMock.Object)
            .Build();

        // Act
        await controller.CreateEmployeeAsync(new CreateEmployeeRequest());

        // Assert
        employeeServiceMock.Verify(s => s.CreateAsync(It.IsAny<EmployeeDto>()), Times.Once);
    }

    [Test]
    public async Task CreateEmployeeAsync_should_return_201_Created_with_location_when_employee_creation_is_successful()
    {
        // Arrange
        var controller = new EmployeesControllerBuilder()
            .Build();

        // Act 
        var result = await controller.CreateEmployeeAsync(new CreateEmployeeRequest());

        //
        result.Should().BeOfType<CreatedAtActionResult>().Which.StatusCode.Should().Be(201);
        result.Should().BeOfType<CreatedAtActionResult>().Which.ActionName.Should().Be("GetById");

    }

    [Test]
    public async Task CreateEmployeeAsync_should_return_expected_employee_response_on_success()
    {
        // Arrange

        //var employeeServiceMock = new Mock<IEmployeeAppService>();
        var _employeeDto = ServiceResult.Success(new EmployeeDtoBuilder().Build());

        var employeeServiceMock = new EmployeeAppServiceMockBuilder()
            .ReturnsForCreateEmployee(_employeeDto)
            .Build();

        var controller = new EmployeesControllerBuilder()
            .Using(employeeServiceMock.Object)
           .Build();

        var expectedResponse = _employeeDto.Value.ToResponse();

        // Act 
        var result = await controller.CreateEmployeeAsync(new CreateEmployeeRequest());

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>().Which.Value.Should().BeOfType<EmployeeResponse>().Which.Should()
            .BeEquivalentTo(expectedResponse);
    }
}
