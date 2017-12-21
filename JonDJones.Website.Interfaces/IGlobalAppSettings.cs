namespace JonDJones.Website.Interfaces
{
    public interface IGlobalAppSettings
    {
        bool RunFixtures { get; }

        bool InDevelopmentMode { get; }
    }
}
