namespace HRCore.EmployeeService.Application.Results;

public class ServiceResult : Result
{
    protected ServiceResult()
    {
    }

    public ErrorType ErrorType { get; init; }

    public static new ServiceResult Success() => new() { Ok = true, ErrorMessage = string.Empty, ErrorType = ErrorType.None };

    public static new ServiceResult<T> Success<T>(T value) => new() { Ok = true, ErrorMessage = string.Empty, ErrorType = ErrorType.None, Value = value };

    public static ServiceResult Fail(string errorMessage, ErrorType errorType) => new() { Ok = false, ErrorMessage = errorMessage, ErrorType = errorType };

    public static ServiceResult<T> Fail<T>(string errorMessage, ErrorType errorType) => new() { Ok = false, ErrorMessage = errorMessage, ErrorType = errorType, Value = default };
}

public class ServiceResult<T> : ServiceResult
{
    protected internal ServiceResult()
    {
    }

    public T Value { get; init; }
}
