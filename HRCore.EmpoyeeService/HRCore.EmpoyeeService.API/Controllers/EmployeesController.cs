using HRCore.EmployeeService.Application.Interfaces;
using HRCore.EmpoyeeService.API.Mapper;
using HRCore.EmpoyeeService.API.Models.Requests;
using HRCore.EmpoyeeService.API.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HRCore.EmpoyeeService.API;

[ApiController]
public class EmployeesController(IEmployeeAppService employeeAppService) : ControllerBase
{
    private readonly IEmployeeAppService _employeeAppService = employeeAppService;


    [HttpPost]
    public async Task<IActionResult> CreateEmployeeAsync([FromBody] CreateEmployeeRequest createEmployeeRequest)
    {
        var result = await _employeeAppService.CreateAsync(createEmployeeRequest.ToDto());

        return CreatedAtAction("GetById", new { id = 111 },
            new EmployeeResponse()
            {
                Id = result.Value.Id,
                FullName = result.Value.FullName,
                Email = result.Value.Email,
                Department = result.Value.Department,
                Role = result.Value.Role,
                Address = result.Value.Address,
                Status = result.Value.Status,
            });
    }
}
