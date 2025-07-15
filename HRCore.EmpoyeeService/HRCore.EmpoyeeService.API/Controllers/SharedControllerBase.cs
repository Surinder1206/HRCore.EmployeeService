using HRCore.EmployeeService.Application.Results;
using Microsoft.AspNetCore.Mvc;

namespace HRCore.EmpoyeeService.API.Controllers
{
    public abstract class SharedControllerBase : ControllerBase
    {
        protected ObjectResult Problem(ServiceResult result)
        {
            if (result.ErrorType == ErrorType.NotFound)
            {
                return Problem(detail: result.ErrorMessage, statusCode: 404);
            }
            else if (result.ErrorType == ErrorType.BadRequest)
            {
                return Problem(detail: result.ErrorMessage, statusCode: 400);
            }

            return Problem(detail: "An internal error has occured, unknown error type.", statusCode: 500);
        }
    }
}
