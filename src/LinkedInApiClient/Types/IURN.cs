namespace LinkedInApiClient.Types
{
    public interface IURN
    {
        string EntityType { get; }
        bool HasValue { get; }
        string Id { get; }
        string Namespace { get; }
    }
}