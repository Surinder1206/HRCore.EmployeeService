using FluentAssertions;
using HRCore.EmployeeService.Application.DTOs;
using HRCore.EmployeeService.Application.Interfaces;
using HRCore.EmployeeService.Application.Results;
using HRCore.EmpoyeeService.API;
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
        var employeeServiceMock = new Mock<IEmployeeAppService>();
        var controller = new EmployeesController(employeeServiceMock.Object);

        // Act
        await controller.CreateEmployeeAsync(new CreateEmployeeRequest());

        // Assert
        employeeServiceMock.Verify(s => s.CreateAsync(It.IsAny<EmployeeDto>()), Times.Once);
    }

    [Test]
    public async Task CreateEmployeeAsync_should_return_201_Created_with_location_when_employee_creation_is_successful()
    {
        // Arrange
        var id = Guid.NewGuid();
        var employeeServiceMock = new Mock<IEmployeeAppService>();
        var _employeeDto = ServiceResult.Success(new EmployeeDto() { Id = id });
        employeeServiceMock.Setup(s => s.CreateAsync(It.IsAny<EmployeeDto>())).ReturnsAsync(_employeeDto);

        var controller = new EmployeesController(employeeServiceMock.Object);

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

        var employeeServiceMock = new Mock<IEmployeeAppService>();
        var _employeeDto = ServiceResult.Success(new EmployeeDto()
        {
            Id = Guid.NewGuid(),
            FullName = "John Doe",
            Email = "john@gmail.com",
            Department = "HR",
            Role = "Developer",
            Address = "10 Queens Apartment",
            Status = "Active",
        });

        var expectedResponse = new EmployeeResponse()
        {
            Id = _employeeDto.Value.Id,
            FullName = _employeeDto.Value.FullName,
            Email = _employeeDto.Value.Email,
            Department = _employeeDto.Value.Department,
            Role = _employeeDto.Value.Role,
            Address = _employeeDto.Value.Address,
            Status = _employeeDto.Value.Status
        };


        employeeServiceMock.Setup(s => s.CreateAsync(It.IsAny<EmployeeDto>())).ReturnsAsync(_employeeDto);
        var controller = new EmployeesController(employeeServiceMock.Object);

        // Act 
        var result = await controller.CreateEmployeeAsync(new CreateEmployeeRequest());

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>().Which.Value.Should().BeOfType<EmployeeResponse>().Which.Should()
            .BeEquivalentTo(expectedResponse);
    }
}
