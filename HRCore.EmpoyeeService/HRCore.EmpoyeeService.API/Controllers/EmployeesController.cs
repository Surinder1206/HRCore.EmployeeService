using HRCore.EmployeeService.Application.Interfaces;
using HRCore.EmpoyeeService.API.Constants;
using HRCore.EmpoyeeService.API.Mapper;
using HRCore.EmpoyeeService.API.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace HRCore.EmpoyeeService.API;

[ApiController]
public class EmployeesController(IEmployeeAppService employeeAppService) : ControllerBase
{
    private readonly IEmployeeAppService _employeeAppService = employeeAppService;

    [HttpPost(ApiEndpoints.Employee.Create)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateEmployeeAsync([FromBody] CreateEmployeeRequest createEmployeeRequest)
    {
        var result = await _employeeAppService.CreateAsync(createEmployeeRequest.ToDto());

        return result.Ok
            ? CreatedAtAction("GetById", new { id = result.Value.Id }, result.Value.ToResponse())
            : Problem(
                detail: result.ErrorMessage,
                statusCode: StatusCodes.Status400BadRequest,
                title: "Bad Request"
            );
    }
}
