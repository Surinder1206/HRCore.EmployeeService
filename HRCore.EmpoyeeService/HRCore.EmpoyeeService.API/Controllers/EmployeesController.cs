using HRCore.EmployeeService.Application.Interfaces;
using HRCore.EmpoyeeService.API.Mapper;
using HRCore.EmpoyeeService.API.Models.Requests;
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

        return CreatedAtAction("GetById", new { id = 111 }, result.Value.ToResponse());
    }
}
