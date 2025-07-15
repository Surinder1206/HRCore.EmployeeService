using HRCore.EmpoyeeService.API.Models.Requests;

namespace HRCore.EmployeeService.Tests.Builders
{
    internal class CreateEmployeeRequestBuilder : Builder<CreateEmployeeRequest>
    {
        private CreateEmployeeRequest _request = new();

        protected override CreateEmployeeRequest OnBuild()
        {
            return _request;
        }
    }
}
