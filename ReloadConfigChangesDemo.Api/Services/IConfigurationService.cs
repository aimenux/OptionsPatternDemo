namespace ReloadConfigChangesDemo.Api.Services
{
    public interface IConfigurationService
    {
        string[] GetValues();
        string GetKeyValue(string key);
    }
}