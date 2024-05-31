# ReloadConfigChangesDemo

In this demo, i m using `IOptions`, `IOptionsMonitor` and `IOptionsSnapshot` to read configuration from settings file.

The strategy is enabled by setting the choice query parameter to :
- `ConfigurationOptionsService` : for reading configuration only at startup
- `ConfigurationOptionsMonitorService` : for reading configuration on each request
- `ConfigurationOptionsSnapshotService` : for reading configuration on each request

**`Tools`** : net 8.0