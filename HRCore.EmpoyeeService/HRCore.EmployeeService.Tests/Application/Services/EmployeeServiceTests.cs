using FluentAssertions;
using HRCore.EmployeeService.Application.Interfaces;
using HRCore.EmployeeService.Application.NotificationMessages;
using HRCore.EmployeeService.Application.Results;
using HRCore.EmployeeService.Domain.Entities;
using HRCore.EmployeeService.Tests.Builders;
using Moq;
using NUnit.Framework;

namespace HRCore.EmployeeService.Tests.Application.Services;

public class EmployeeServiceTests
{
    [Test]
    public async Task CreateAsync_Should_call_insert_async_and_save_async_when_request_is_valid()
    {
        // Arrange
        var employeeRepository = new EmployeeRepositoryMockBuilder()
            .ReturnsForFirstOrDefaultAsync(null!)
            .Build();

        var unitOfWorkMockBuilder = new UnitOfWorkMockBuilder()
            .Using(employeeRepository.Object)
            .Build();

        var service = new EmployeeAppServiceBuilder()
            .WithUnitOfWork(unitOfWorkMockBuilder.Object)
            .Build();

        // Act
        var result = await service.CreateAsync(new EmployeeDtoBuilder().Build());

        // Assert
        unitOfWorkMockBuilder.Verify(u => u.EmployeeRepository.InsertAsync(It.IsAny<Employee>()), Times.Once);
        unitOfWorkMockBuilder.Verify(u => u.SaveAsync(), Times.Once);
    }

    [Test]
    public async Task CreateAsync_Should_call_SendCreateEmployeeMessage_when_employee_created()
    {
        // Arrange
        var messagingServiceMock = new Mock<IMessagingService>();

        var employeeRepository = new EmployeeRepositoryMockBuilder()
            .ReturnsForFirstOrDefaultAsync(null!)
            .Build();

        var unitOfWorkMockBuilder = new UnitOfWorkMockBuilder()
            .Using(employeeRepository.Object)
            .Build();

        var service = new EmployeeAppServiceBuilder()
            .WithUnitOfWork(unitOfWorkMockBuilder.Object)
           .WithMessagingService(messagingServiceMock.Object)
           .Build();

        // Act
        var result = await service.CreateAsync(new EmployeeDtoBuilder().Build());

        // Assert
        messagingServiceMock.Verify(m => m.SendCreateEmployeeMessage(It.IsAny<EmployeeMessageBody>()), Times.Once);
    }

    [Test]
    public async Task CreateAsync_Should_return_employee_dto_with_expected_values()
    {
        // Arrange
        var employeeDto = new EmployeeDtoBuilder().Build();

        var employeeRepository = new EmployeeRepositoryMockBuilder()
            .ReturnsForFirstOrDefaultAsync(null!)
            .Build();

        var unitOfWorkMockBuilder = new UnitOfWorkMockBuilder()
            .Using(employeeRepository.Object)
            .Build();

        var service = new EmployeeAppServiceBuilder()
            .WithUnitOfWork(unitOfWorkMockBuilder.Object)
            .Build();

        // Act
        var result = await service.CreateAsync(new EmployeeDtoBuilder().Build());

        // Assert
        result.Should().NotBeNull();
        result.Value.FullName.Should().Be(employeeDto.FullName);
        result.Value.Department.Should().Be(employeeDto.Department);
        result.Value.Email.Should().Be(employeeDto.Email);
        result.Value.Role.Should().Be(employeeDto.Role);
        result.Value.Address.Should().Be(employeeDto.Address);
        result.Value.DateOfJoining.Should().Be(employeeDto.DateOfJoining);
        result.Value.Status.Should().Be(employeeDto.Status);
    }

    [Test]
    public async Task CreateAsync_should_return_an_error_when_employee_with_same_email_exists()
    {
        // Arrange
        var existingEmployee = new EmployeeBuilder()
            .WithEmail("abc@gmail.com")
            .Build();

        var employeeRepositoryMock = new EmployeeRepositoryMockBuilder()
            .ReturnsForFirstOrDefaultAsync(existingEmployee)
            .Build();

        var unitOfWorkMock = new UnitOfWorkMockBuilder()
            .Using(employeeRepositoryMock.Object)
            .Build();

        var service = new EmployeeAppServiceBuilder()
            .WithUnitOfWork(unitOfWorkMock.Object)
            .Build();

        // Act
        var result = await service.CreateAsync(new EmployeeDtoBuilder().Build());

        // Assert
        result.Failed.Should().BeTrue();
        result.ErrorType.Should().Be(ErrorType.BadRequest);
        result.ErrorMessage.Should().Be("An employee with the same email already exists.");
    }
}