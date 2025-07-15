using HRCore.EmployeeService.Application.DTOs;
using HRCore.EmployeeService.Application.Interfaces;
using HRCore.EmpoyeeService.API.Constants;
using HRCore.EmpoyeeService.API.Controllers;
using HRCore.EmpoyeeService.API.Mapper;
using HRCore.EmpoyeeService.API.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace HRCore.EmpoyeeService.API;

[ApiController]
public class EmployeesController(IEmployeeAppService employeeAppService) : SharedControllerBase
{
    private readonly IEmployeeAppService _employeeAppService = employeeAppService;

    [HttpPost(ApiEndpoints.Employee.Create)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces(typeof(ActionResult<EmployeeDto>))]
    public async Task<IActionResult> CreateEmployeeAsync([FromBody] CreateEmployeeRequest createEmployeeRequest)
    {
        var result = await _employeeAppService.CreateAsync(createEmployeeRequest.ToDto());

        return result.Ok
            ? CreatedAtAction("GetById", new { id = result.Value.Id }, result.Value.ToResponse())
            : Problem(result);
    }

    [HttpGet(ApiEndpoints.Employee.Get)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces(typeof(ActionResult<EmployeeDto>))]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _employeeAppService.GetEmployeeByIdAsync(id);
        return result.Ok
            ? Ok(result.Value)
            : Problem(result);
    }

    [HttpGet(ApiEndpoints.Employee.GetAll)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Produces(typeof(ActionResult<List<EmployeeDto>>))]
    public async Task<IActionResult> GetAll()
    {
        var result = await _employeeAppService.GetAllEmployeesAsync();
        return result.Ok
            ? Ok(result.Value)
            : Problem(result);
    }

    [HttpPut(ApiEndpoints.Employee.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces(typeof(ActionResult<EmployeeDto>))]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateEmployeeRequest updateEmployeeRequest)
    {
        var result = await _employeeAppService.UpdateEmployeeAsync(id, updateEmployeeRequest.ToDto());
        return result.Ok
            ? NoContent()
            : Problem(result);
    }
}
