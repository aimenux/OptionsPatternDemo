using System;
using System.Globalization;
using Microsoft.Extensions.Options;

namespace ReloadConfigChangesDemo.Api.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IOptions<Flags> _options;
        private readonly IOptionsSnapshot<Flags> _optionsSnapshot;
        private readonly IOptionsMonitor<Flags> _optionsMonitor;

        public ConfigurationService(IOptions<Flags> options)
        {
            _options = options;
        }

        public ConfigurationService(IOptionsSnapshot<Flags> options)
        {
            _optionsSnapshot = options;
        }

        public ConfigurationService(IOptionsMonitor<Flags> options)
        {
            _optionsMonitor = options;
        }

        public string[] GetAllKeys()
        {
            return new[]
            {
                nameof(Flags.Feature1), 
                nameof(Flags.Feature2)
            };
        }

        public string[] GetAllValues()
        {
            return new[]
            {
                GetFlags().Feature1, 
                GetFlags().Feature2
            };
        }

        public string GetKeyValue(string key)
        {
            switch (ToTitleCase(key))
            {
                case nameof(Flags.Feature1):
                    return GetFlags().Feature1;
                case nameof(Flags.Feature2):
                    return GetFlags().Feature2;
                default:
                    throw new ArgumentException($"Unfound key {key}");
            }
        }

        private Flags GetFlags()
        {
            if (_options != null) return _options.Value;
            return _optionsSnapshot != null ? _optionsSnapshot.Value : _optionsMonitor?.CurrentValue;
        }

        private static string ToTitleCase(string input)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        }
    }
}
