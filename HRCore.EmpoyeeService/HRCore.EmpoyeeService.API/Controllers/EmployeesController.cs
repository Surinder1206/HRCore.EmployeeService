using Asp.Versioning;
using HRCore.EmployeeService.Application.Interfaces;
using HRCore.EmpoyeeService.API.Constants;
using HRCore.EmpoyeeService.API.Controllers;
using HRCore.EmpoyeeService.API.Mapper;
using HRCore.EmpoyeeService.API.Models.Requests;
using HRCore.EmpoyeeService.API.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRCore.EmpoyeeService.API;

[ApiVersion(1.0)]
[ApiVersion(2.0)]
[ApiController]
[Authorize()]
public class EmployeesController(IEmployeeAppService employeeAppService) : SharedControllerBase
{
    private readonly IEmployeeAppService _employeeAppService = employeeAppService;

    [MapToApiVersion(1.0)]
    [EndpointDescription("Creates a new employee record.")]
    [HttpPost(ApiEndpoints.Employee.Create)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces(typeof(ActionResult<EmployeeResponse>))]
    public async Task<IActionResult> CreateEmployeeAsync([FromBody] CreateEmployeeRequest createEmployeeRequest)
    {
        var result = await _employeeAppService.CreateAsync(createEmployeeRequest.ToDto());

        return result.Ok
            ? CreatedAtAction("GetById", new { id = result.Value.Id }, result.Value.ToResponse())
            : Problem(result.ToProblemResponse());
    }


    [MapToApiVersion(1.0)]
    [EndpointDescription("Retrieves an employee by their unique identifier.")]
    [HttpGet(ApiEndpoints.Employee.Get)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces(typeof(ActionResult<EmployeeResponse>))]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _employeeAppService.GetEmployeeByIdAsync(id);
        return result.Ok
            ? Ok(result.Value.ToResponse())
            : Problem(result.ToProblemResponse());
    }

    [Authorize(Roles = "HRAdmin")]
    [MapToApiVersion(1.0)]
    [EndpointDescription("Retrieves all employees in the system.")]
    [HttpGet(ApiEndpoints.Employee.GetAll)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Produces(typeof(ActionResult<List<EmployeeResponse>>))]
    public async Task<IActionResult> GetAll()
    {
        var result = await _employeeAppService.GetAllEmployeesAsync();

        return result.Ok
            ? Ok(result.Value.Select(x => x.ToResponse()).ToList())
            : Problem(result);
    }

    [MapToApiVersion(1.0)]
    [EndpointDescription("Updates an existing employee's details.")]
    [HttpPut(ApiEndpoints.Employee.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateEmployeeRequest updateEmployeeRequest)
    {
        var result = await _employeeAppService.UpdateEmployeeAsync(id, updateEmployeeRequest.ToDto());
        return result.Ok
            ? NoContent()
            : Problem(result);
    }

    [MapToApiVersion(1.0)]
    [EndpointDescription("Deletes an employee by their unique identifier.")]
    [HttpDelete(ApiEndpoints.Employee.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await _employeeAppService.DeleteEmployeeAsync(id);
        return result.Ok
            ? NoContent()
            : Problem(result);
    }
}
