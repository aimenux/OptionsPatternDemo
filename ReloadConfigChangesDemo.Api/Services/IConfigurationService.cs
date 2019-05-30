namespace ReloadConfigChangesDemo.Api.Services
{
    public interface IConfigurationService
    {
        string[] GetAllKeys();
        string[] GetAllValues();
        string GetKeyValue(string key);
    }
}