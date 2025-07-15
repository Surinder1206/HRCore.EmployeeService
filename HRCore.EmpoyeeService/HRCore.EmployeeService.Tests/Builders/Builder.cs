namespace HRCore.EmployeeService.Tests.Builders;

public abstract class Builder<TObject>
{
    public TObject Build()
    {
        return OnBuild();
    }

    protected abstract TObject OnBuild();
}
