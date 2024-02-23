namespace TrustGuard_API.Configurations;

public class MongoSettings
{
    public string ConenctionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string CollectionName { get; set; } = null!;
}