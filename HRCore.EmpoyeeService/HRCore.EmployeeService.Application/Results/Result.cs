namespace HRCore.EmployeeService.Application.Results;

public class Result
{
    protected Result()
    {
    }

    public bool Ok { get; init; }

    public string ErrorMessage { get; init; }

    public bool Failed => !Ok;

    public static Result Success() => new() { Ok = true, ErrorMessage = string.Empty };

    public static Result<T> Success<T>(T value) => new() { Ok = true, ErrorMessage = string.Empty, Value = value };

    public static Result Fail(string errorMessage) => new() { Ok = false, ErrorMessage = errorMessage };

    public static Result<T> Fail<T>(string errorMessage) => new() { Ok = false, ErrorMessage = errorMessage, Value = default };
}

public class Result<T> : Result
{
    protected internal Result()
    {
    }

    public T Value { get; init; }
}
