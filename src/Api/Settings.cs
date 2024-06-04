namespace Api;

public sealed record Settings
{
    public const string ApplicationName = "OptionsPatternDemo";
    public string Entry1 { get; init; }
    public string Entry2 { get; init; }
}

public enum Choices
{
    ConfigurationOptionsService = 1,
    ConfigurationOptionsMonitorService,
    ConfigurationOptionsSnapshotService
}