# ReloadConfigChangesDemo

[Options pattern in net core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options)

In this demo, we are using `IOptions`, `IOptionsMonitor` and `IOptionsSnapshot` in a web api net core 2.2

You need to set options strategy by setting choice configuration variable :
- (1) : for `IOptions`
- (2) : for `IOptionsMonitor`
- (3) : for `IOptionsSnapshot`

**`Tools`** : vs19, net core 2.2