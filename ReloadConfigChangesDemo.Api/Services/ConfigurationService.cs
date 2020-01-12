using System;
using System.Globalization;
using Microsoft.Extensions.Options;

namespace ReloadConfigChangesDemo.Api.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly Features _features;
        private readonly IOptions<Features> _options;
        private readonly IOptionsSnapshot<Features> _optionsSnapshot;
        private readonly IOptionsMonitor<Features> _optionsMonitor;

        public ConfigurationService(IOptions<Features> options)
        {
            _options = options;
            _features = options.Value;
        }

        public ConfigurationService(IOptionsSnapshot<Features> options)
        {
            _optionsSnapshot = options;
            _features = options.Value;
        }

        public ConfigurationService(IOptionsMonitor<Features> options)
        {
            _optionsMonitor = options;
            _features = options.CurrentValue;
        }

        public string[] GetValues()
        {
            return new[]
            {
                _features.Feature1, 
                _features.Feature2
            };
        }

        public string GetKeyValue(string key)
        {
            switch (ToTitleCase(key))
            {
                case nameof(Features.Feature1):
                    return _features.Feature1;
                case nameof(Features.Feature2):
                    return _features.Feature2;
                default:
                    throw new ArgumentException($"Unfound key {key}");
            }
        }

        private static string ToTitleCase(string input)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        }
    }
}
