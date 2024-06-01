[![.NET](https://github.com/aimenux/ReloadConfigChangesDemo/actions/workflows/ci.yml/badge.svg?branch=master)](https://github.com/aimenux/ReloadConfigChangesDemo/actions/workflows/ci.yml)

# OptionsPatternDemo
```
Using options pattern in web api
```

In this demo, i m using [OptionsPattern](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-8.0#options-interfaces) (`IOptions`, `IOptionsMonitor` and `IOptionsSnapshot`) to read settings from configuration file.

The strategy is enabled by setting the choice query parameter to :
- `ConfigurationOptionsService` : for reading values once at the startup (reload configuration only after startup)
- `ConfigurationOptionsMonitorService` : for reading always new values (reload configuration in every usage even in the same request)
- `ConfigurationOptionsSnapshotService` : for reading new values for each new request (reload configuration in every new request)

**`Tools`** : net 8.0, integration-tests