using HRCore.EmployeeService.Application.Interfaces;
using HRCore.EmpoyeeService.API.Mapper;
using HRCore.EmpoyeeService.API.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace HRCore.EmpoyeeService.API;

public class EmployeesController(IEmployeeAppService employeeAppService)
{
    private readonly IEmployeeAppService _employeeAppService = employeeAppService;


    [HttpPost]
    public async Task<IActionResult> CreateEmployeeAsync([FromBody] CreateEmployeeRequest createEmployeeRequest)
    {
        var result = await _employeeAppService.CreateAsync(createEmployeeRequest.ToDto());
        return null;
    }
}
