namespace HRCore.EmpoyeeService.API.Constants
{
    public class ApiEndpoints
    {
        private const string ApiBase = "api";

        public static class Employee
        {
            private const string Base = $"{ApiBase}/employees";

            public const string Create = Base;
            public const string Get = $"{Base}/{{id:guid}}";
            public const string GetAll = $"{Base}";
            public const string Update = $"{Base}/{{id:guid}}";
        }
    }

}
