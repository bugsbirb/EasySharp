First .NET project, so be kind It's an API wrapper for Easypanel

### Install
```
dotnet add package EasySharp
```

### Basic Use
```c#
using EasySharp;

var api = new Easypanel("Easypanel Panel URL", "API token");
```

```c#
var deploy = await api.DeployServiceAsync(
    projectName: "Cool Project",
    serviceName: "Egg",
    forceRebuild: false);

if (deploy.success)
{
    Console.WriteLine("Deployment succeeded.");
}
else
{
    Console.WriteLine("Deployment failed.");
}
```

# Function Map

## Apps
- `GetAppsAsync`
- `StartAppAsync`
- `StopAppAsync`
- `DestroyAppAsync`
- `CreateAppAsync`
- `DeployServiceAsync`
- `RefreshDeployTokenAsync`
- `EnableGithubDeployAsync`
- `DisableGithubDeployAsync`

## Users
- `GetUserAsync`
- `CreateUserAsync`

## Compose
- `DeployComposeAsync`
- `StartComposeAsync`
- `StopComposeAsync`
- `DestroyComposeAsync`

## Other
- `GetProjectsAsync`
- `GetSystemStatsAsync`
