namespace Api.Security;

public class SecurityOptions
{
    public const string Key = "AzureAd";

    public string SwaggerClientId { get; set; } = string.Empty;
    public string Instance { get; set; } = string.Empty;
    public string TenantId { get; set; } = string.Empty;
    public object Audience { get; set; } = string.Empty;
}
