namespace HRCore.EmpoyeeService.API.Options;

public class AzureAdOptions
{
    public string Audience { get; init; }

    //public string ClientId { get; init; }

    //public string ClientSecret { get; init; }

    public string Instance { get; init; }

    public string TenantId { get; init; }

    public string Authority
    {
        get
        {
            var separator = Instance.EndsWith('/') ? string.Empty : "/";
            return $"{Instance}{separator}{TenantId}";
        }
    }
}
