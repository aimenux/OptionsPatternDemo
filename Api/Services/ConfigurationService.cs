using Microsoft.Extensions.Options;

namespace Api.Services;

public class ConfigurationService : IConfigurationService
{
    private readonly Settings _settings;

    public ConfigurationService(IOptions<Settings> options)
    {
        _settings = options.Value;
    }

    public ConfigurationService(IOptionsSnapshot<Settings> options)
    {
        _settings = options.Value;
    }

    public ConfigurationService(IOptionsMonitor<Settings> options)
    {
        _settings = options.CurrentValue;
    }

    public string[] GetEntries()
    {
        return
        [
            _settings.Entry1, 
            _settings.Entry2
        ];
    }
}