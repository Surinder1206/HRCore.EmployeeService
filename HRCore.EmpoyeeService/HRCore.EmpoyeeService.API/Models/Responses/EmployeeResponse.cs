namespace HRCore.EmpoyeeService.API.Models.Responses;

public class EmployeeResponse
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Department { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string Address { get; set; }
    public DateTime DateOfJoining { get; set; }
    public string Status { get; set; }
}

