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

        unitOfWorkMock.Setup(u => u.EmployeeRepository.InsertAsync(It.IsAny<Employee>()))
            .Returns(Task.CompletedTask);

        var service = new EmployeeAppService(unitOfWorkMock.Object);

        // Act
        var result = await service.CreateAsync(dto);

        // Assert
        unitOfWorkMock.Verify(u => u.EmployeeRepository.InsertAsync(It.IsAny<Employee>()), Times.Once);
        unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
    }
}
