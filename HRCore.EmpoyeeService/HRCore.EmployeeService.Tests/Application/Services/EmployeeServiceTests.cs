using FluentAssertions;
using HRCore.EmployeeService.Application.DTOs;
using HRCore.EmployeeService.Application.Interfaces;
using HRCore.EmployeeService.Application.NotificationMessages;
using HRCore.EmployeeService.Application.Services;
using HRCore.EmployeeService.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace HRCore.EmployeeService.Tests.Application.Services;

public class EmployeeServiceTests
{
    [Test]
    public async Task CreateAsync_Should_call_insert_async_and_save_async_when_request_is_valid()
    {
        // Arrange
        var dto = new EmployeeDto
        {
            FullName = "Surinder Singh",
            Department = "Engineering",
            Email = "surinder.singh@gmail.com",
            Role = "Software Engineer",
            Address = "123 Main St, City, Country",
            DateOfJoining = DateTime.UtcNow,
            Status = "Active"
        };

        var unitOfWorkMock = new Mock<IUnitOfWork>();

        unitOfWorkMock.Setup(u => u.EmployeeRepository.InsertAsync(It.IsAny<Employee>()));


        var service = new EmployeeAppService(unitOfWorkMock.Object, new Mock<IMessagingService>().Object);

        // Act
        var result = await service.CreateAsync(dto);

        // Assert
        unitOfWorkMock.Verify(u => u.EmployeeRepository.InsertAsync(It.IsAny<Employee>()), Times.Once);
        unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
    }

    [Test]
    public async Task CreateAsync_Should_call_SendCreateEmployeeMessage_when_employee_created()
    {
        // Arrange
        var dto = new EmployeeDto
        {
            FullName = "Surinder Singh",
            Department = "Engineering",
            Email = "surinder.singh@gmail.com",
            Role = "Software Engineer",
            Address = "123 Main St, City, Country",
            DateOfJoining = DateTime.UtcNow,
            Status = "Active"
        };

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(u => u.EmployeeRepository.InsertAsync(It.IsAny<Employee>()));

        var messagingServiceMock = new Mock<IMessagingService>();

        var service = new EmployeeAppService(unitOfWorkMock.Object, messagingServiceMock.Object);

        // Act
        var result = await service.CreateAsync(dto);

        // Assert
        messagingServiceMock.Verify(m => m.SendCreateEmployeeMessage(It.IsAny<EmployeeMessageBody>()), Times.Once);
    }

    [Test]
    public async Task CreateAsync_Should_return_employee_dto_with_expected_values()
    {
        // Arrange
        var dto = new EmployeeDto
        {
            FullName = "Surinder Singh",
            Department = "Engineering",
            Email = "surinder.singh@gmail.com",
            Role = "Software Engineer",
            Address = "123 Main St, City, Country",
            DateOfJoining = DateTime.UtcNow,
            Status = "Active"
        };

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(u => u.EmployeeRepository.InsertAsync(It.IsAny<Employee>()));

        var messagingServiceMock = new Mock<IMessagingService>();
        var service = new EmployeeAppService(unitOfWorkMock.Object, messagingServiceMock.Object);

        // Act
        var result = await service.CreateAsync(dto);

        // Assert
        result.Should().NotBeNull();
        result.FullName.Should().Be(dto.FullName);
        result.Department.Should().Be(dto.Department);
        result.Email.Should().Be(dto.Email);
        result.Role.Should().Be(dto.Role);
        result.Address.Should().Be(dto.Address);
        result.DateOfJoining.Should().Be(dto.DateOfJoining);
        result.Status.Should().Be(dto.Status);
    }
}