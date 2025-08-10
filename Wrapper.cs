using System.Text.Json;
using System.Text.Json.Serialization;

namespace EasySharp
{
    public class Easypanel
    {
        private readonly HttpClient _client;

        public Easypanel(string baseUrl, string? apiKey = null)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseUrl);

            if (!string.IsNullOrEmpty(apiKey))
            {
                _client.DefaultRequestHeaders.Remove("Authorization");
                _client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", apiKey);
            }
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(
                "/api/trpc/projects.listProjectsAndServices"
            );
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                string requestUri = response.RequestMessage?.RequestUri?.ToString() ?? "unknown";
                throw new Exception(
                    $"[Easypanel API] {requestUri} failed with status {response.StatusCode} | {errorContent}"
                );
            }
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            RootResponse<JsonData>? rootResponse = JsonSerializer.Deserialize<RootResponse<JsonData>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return rootResponse?.Result?.Data?.Json?.Projects ?? new List<Project>();
        }

        public async Task<List<Service>> GetAppsAsync()
        {
            HttpResponseMessage Response = await _client.GetAsync(
                "/api/trpc/projects.listProjectsAndServices"
            );
            if (!Response.IsSuccessStatusCode)
            {
                string errorContent = await Response.Content.ReadAsStringAsync();
                string requestUri = Response.RequestMessage?.RequestUri?.ToString() ?? "unknown";
                throw new Exception(
                    $"[Easypanel API] {requestUri} failed with status {Response.StatusCode} | {errorContent}"
                );
            }
            Response.EnsureSuccessStatusCode();

            string json = await Response.Content.ReadAsStringAsync();
            RootResponse<Root>? rootResponse = JsonSerializer.Deserialize<RootResponse<Root>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                }
            );
            return rootResponse?.Result?.Data?.Json?.services ?? new List<Service>();
        }

        public async Task<Service> GetAppAsync(
            string projectName,
            string serviceName
        )
        {
            HttpResponseMessage response = await _client.GetAsync(
                $"/api/trpc/projects.inspectService?input={{\"json\":{{\"projectName\":\"{projectName}\",\"serviceName\":\"{serviceName}\"}}}}"
            );
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                string requestUri = response.RequestMessage?.RequestUri?.ToString() ?? "unknown";
                throw new Exception(
                    $"[Easypanel API] {requestUri} failed with status {response.StatusCode} | {errorContent}"
                );
            }
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            RootResponse<Service>? rootResponse = JsonSerializer.Deserialize<RootResponse<Service>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
            return rootResponse?.Result?.Data?.Json ?? new Service();
            
        }

        public async Task<Project> GetProjectAsync(
            string projectName
        )
        {
            HttpResponseMessage response = await _client.GetAsync(
                $"/api/trpc/projects.inspectProject?input={{\"json\":{{\"projectName\":\"{projectName}\"}}}}"
            );
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                string requestUri = response.RequestMessage?.RequestUri?.ToString() ?? "unknown";
                throw new Exception(
                    $"[Easypanel API] {requestUri} failed with status {response.StatusCode} | {errorContent}"
                );
            }
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            RootResponse<Project>? rootResponse = JsonSerializer.Deserialize<RootResponse<Project>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
            return rootResponse?.Result?.Data?.Json ?? new Project { Name = string.Empty };
            
        }

        public async Task<User> GetUserAsync()
        {
            HttpResponseMessage response = await _client.GetAsync("api/trpc/auth.getUser");
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                string requestUri = response.RequestMessage?.RequestUri?.ToString() ?? "unknown";
                throw new Exception(
                    $"[Easypanel API] {requestUri} failed with status {response.StatusCode} | {errorContent}"
                );
            }
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            RootResponse<User>? rootResponse = JsonSerializer.Deserialize<RootResponse<User>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
            return rootResponse?.Result?.Data?.Json
                ?? new User
                {
                    Id = string.Empty,
                    CreatedAt = DateTime.MinValue,
                    Email = string.Empty,
                    Admin = false,
                    ApiToken = string.Empty,
                };
        }

        public async Task<bool> DeployComposeAsync(
            string projectName,
            string serviceName,
            bool forceRebuild = false
        )
        {
            ServicePayload payload = new ServicePayload
                {
                    projectName = projectName,
                    serviceName = serviceName,
                    forceRebuild = forceRebuild
                };

            PayloadWrapper<ServicePayload> Tiedup = new PayloadWrapper<ServicePayload>
                {
                    json = payload
                };

            StringContent content = new StringContent(
                JsonSerializer.Serialize(Tiedup),
                System.Text.Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage response = await _client.PostAsync(
                "/api/trpc/services.compose.deployService",
                content
            );
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                string requestUri = response.RequestMessage?.RequestUri?.ToString() ?? "unknown";
                throw new HttpRequestException(
                    $"[Easypanel API] {requestUri} failed with status {response.StatusCode} | {errorContent}"
                );
            }
            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<bool> StopComposeAsync(string projectName, string serviceName)
        {
            ServicePayload payload = new ServicePayload
                {
                    projectName = projectName,
                    serviceName = serviceName,
                };
            PayloadWrapper<ServicePayload> Tiedup = new PayloadWrapper<ServicePayload>
                {
                    json = payload
                };

            StringContent content = new StringContent(
                JsonSerializer.Serialize(Tiedup),
                System.Text.Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage response = await _client.PostAsync(
                "/api/trpc/services.compose.stopService",
                content
            );
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                string requestUri = response.RequestMessage?.RequestUri?.ToString() ?? "unknown";
                throw new HttpRequestException(
                    $"[Easypanel API] {requestUri} failed with status {response.StatusCode} | {errorContent}"
                );
            }
        
            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<bool> DestroyComposeAsync(string projectName, string serviceName)
        {
            ServicePayload payload = new ServicePayload
                    {
                        projectName = projectName,
                        serviceName = serviceName,
                    };

            PayloadWrapper<ServicePayload> Tiedup = new PayloadWrapper<ServicePayload>
                    {
                        json = payload
                    };
            StringContent content = new StringContent(
                JsonSerializer.Serialize(Tiedup),
                System.Text.Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage response = await _client.PostAsync(
                "/api/trpc/services.compose.destroyService",
                content
            );
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                string requestUri = response.RequestMessage?.RequestUri?.ToString() ?? "unknown";
                throw new HttpRequestException(
                    $"[Easypanel API] {requestUri} failed with status {response.StatusCode} | {errorContent}"
                );
            }
            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<bool> StartComposeAsync(string projectName, string serviceName)
        {
            ServicePayload payload = new ServicePayload
                {
                    projectName = projectName,
                    serviceName = serviceName,
                };

            PayloadWrapper<ServicePayload> Tiedup = new PayloadWrapper<ServicePayload>
                {
                    json = payload
                };

            StringContent content = new StringContent(
                JsonSerializer.Serialize(Tiedup),
                System.Text.Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage response = await _client.PostAsync(
                "/api/trpc/services.compose.startService",
                content
            );
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                string requestUri = response.RequestMessage?.RequestUri?.ToString() ?? "unknown";
                throw new HttpRequestException(
                    $"[Easypanel API] {requestUri} failed with status {response.StatusCode} | {errorContent}"
                );
            }
            response.EnsureSuccessStatusCode();
            return true;;
        }
        
        public async Task<bool> DeployAppAsync(
            string projectName,
            string serviceName,
            bool forceRebuild = false
        )
        {
            ServicePayload payload = new ServicePayload
            {
                projectName = projectName,
                serviceName = serviceName,
                forceRebuild = forceRebuild
            };

            PayloadWrapper<ServicePayload> TiedUp = new PayloadWrapper<ServicePayload>
            {
                json = payload
            };

            StringContent content = new StringContent(
                JsonSerializer.Serialize(TiedUp),
                System.Text.Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage response = await _client.PostAsync(
                "/api/trpc/services.app.deployService",
                content
            );
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                string requestUri = response.RequestMessage?.RequestUri?.ToString() ?? "unknown";
                throw new HttpRequestException(
                    $"[Easypanel API] {requestUri} failed with status {response.StatusCode} | {errorContent}"
                );
            }
            response.EnsureSuccessStatusCode();
            return true;
        }


        public async Task<bool> SetServiceNote(string projectName, string serviceName, string note)
        {
            ServicePayload payload = new ServicePayload
            {
                projectName = projectName,
                serviceName = serviceName,
                notes = note
            };
            PayloadWrapper<ServicePayload> Tiedup = new PayloadWrapper<ServicePayload>
            {
                json = payload
            };
            StringContent content = new StringContent(
                JsonSerializer.Serialize(Tiedup),
                System.Text.Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage response = await _client.PostAsync("/api/trpc/services.common.setName", content);
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                string requestUri = response.RequestMessage?.RequestUri?.ToString() ?? "unknown";
                throw new HttpRequestException(
                    $"[Easypanel API] {requestUri} failed with status {response.StatusCode} | {errorContent}"
                );
            }
            response.EnsureSuccessStatusCode();
            return true;

        }

        public async Task<bool> StartAppAsync(string projectName, string serviceName)
        {
            ServicePayload payload = new ServicePayload
            {
                projectName = projectName,
                serviceName = serviceName,
            };
            PayloadWrapper<ServicePayload> Tiedup = new PayloadWrapper<ServicePayload>
            {
                json = payload
            };

            StringContent content = new StringContent(
                JsonSerializer.Serialize(Tiedup),
                System.Text.Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage response = await _client.PostAsync(
                "/api/trpc/services.app.startService",
                content
            );
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                string requestUri = response.RequestMessage?.RequestUri?.ToString() ?? "unknown";
                throw new HttpRequestException(
                    $"[Easypanel API] {requestUri} failed with status {response.StatusCode} | {errorContent}"
                );
            }
            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<bool> StopAppAsync(string projectName, string serviceName)
        {
            ServicePayload payload = new ServicePayload
            {
                projectName = projectName,
                serviceName = serviceName,
            };

            PayloadWrapper<ServicePayload> Tiedup = new PayloadWrapper<ServicePayload>
            {
                json = payload
            };
            StringContent content = new StringContent(
                JsonSerializer.Serialize(Tiedup),
                System.Text.Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage response = await _client.PostAsync(
                "/api/trpc/services.app.stopService",
                content
            );
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                string requestUri = response.RequestMessage?.RequestUri?.ToString() ?? "unknown";
                throw new HttpRequestException(
                    $"[Easypanel API] {requestUri} failed with status {response.StatusCode} | {errorContent}"
                );
            }
            response.EnsureSuccessStatusCode();
            return true;;
        }

        public async Task<bool> DestroyAppAsync(string projectName, string serviceName)
        {
            ServicePayload payload = new ServicePayload
            {
                projectName = projectName,
                serviceName = serviceName,
            };
            PayloadWrapper<ServicePayload> Tiedup = new PayloadWrapper<ServicePayload>
            {
                json = payload
            };
            StringContent content = new StringContent(
                JsonSerializer.Serialize(Tiedup),
                System.Text.Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage response = await _client.PostAsync(
                "/api/trpc/services.app.destroyService",
                content
            );
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                string requestUri = response.RequestMessage?.RequestUri?.ToString() ?? "unknown";
                throw new HttpRequestException(
                    $"[Easypanel API] {requestUri} failed with status {response.StatusCode} | {errorContent}"
                );
            }
            response.EnsureSuccessStatusCode();
            return true;;
        }

        public async Task<bool> RefreshDeployTokenAsync(string projectName, string serviceName)
        {
            ServicePayload payload = new ServicePayload
            {
                projectName = projectName,
                serviceName = serviceName,
            };

            PayloadWrapper<ServicePayload> Tiedup = new PayloadWrapper<ServicePayload>
            {
                json = payload
            };
            StringContent content = new StringContent(
                JsonSerializer.Serialize(Tiedup),
                System.Text.Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage response = await _client.PostAsync(
                "/api/trpc/services.app.refreshDeployToken",
                content
            );
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                string requestUri = response.RequestMessage?.RequestUri?.ToString() ?? "unknown";
                throw new HttpRequestException(
                    $"[Easypanel API] {requestUri} failed with status {response.StatusCode} | {errorContent}"
                );
            }
            response.EnsureSuccessStatusCode();
            return true;;
        }

        public async Task<bool> EnableGithubDeployAsync(string projectName, string serviceName)
        {
            ServicePayload payload = new ServicePayload
            {
                projectName = projectName,
                serviceName = serviceName,
            };

            PayloadWrapper<ServicePayload> Tiedup = new PayloadWrapper<ServicePayload>
            {
                json = payload
            };
            StringContent content = new StringContent(
                JsonSerializer.Serialize(Tiedup),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage response = await _client.PostAsync(
                "/api/trpc/services.app.enableGithubDeploy",
                content
            );

            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                string requestUri = response.RequestMessage?.RequestUri?.ToString() ?? "unknown";
                throw new HttpRequestException(
                    $"[Easypanel API] {requestUri} failed with status {response.StatusCode} | {errorContent}"
                );
            }

            return true;;
        }

        public async Task<bool> DisableGithubDeployAsync(string projectName, string serviceName)
        {
            ServicePayload payload = new ServicePayload
            {
                projectName = projectName,
                serviceName = serviceName,
            };

            PayloadWrapper<ServicePayload> Tiedup = new PayloadWrapper<ServicePayload>
            {
                json = payload
            };
            StringContent content = new StringContent(
                JsonSerializer.Serialize(Tiedup),
                System.Text.Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage response = await _client.PostAsync(
                "/api/trpc/services.app.disableGithubDeploy",
                content
            );
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                string requestUri = response.RequestMessage?.RequestUri?.ToString() ?? "unknown";
                throw new HttpRequestException(
                    $"[Easypanel API] {requestUri} failed with status {response.StatusCode} | {errorContent}"
                );
            }
            response.EnsureSuccessStatusCode();
            return true;;
        }

        public async Task<bool> CreateUserAsync(string email, string password, bool? admin)
        {
            UserPayloadTiedUp Tiedup = new UserPayloadTiedUp
            {
                json = new UserPayload
                {
                    email = email,
                    password = password,
                    admin = admin ?? false
                }
            };
            StringContent content = new StringContent(
                JsonSerializer.Serialize(Tiedup),
                System.Text.Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage response = await _client.PostAsync(
                "/api/trpc/users.createUser",
                content
            );
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                string requestUri = response.RequestMessage?.RequestUri?.ToString() ?? "unknown";
                throw new HttpRequestException(
                    $"[Easypanel API] {requestUri} failed with status {response.StatusCode} | {errorContent}"
                );
            }
            response.EnsureSuccessStatusCode();
            return true;;
        }

        public async Task<bool> CreateAppAsync(
            string projectName,
            string serviceName,
            string? sourceType = null,
            string? sourceImage = null,
            string? sourceUsername = null,
            string? sourcePassword = null,
            string? buildType = null,
            string? buildFile = null,
            string? env = null,
            IEnumerable<(string username, string password)>? basicAuth = null,
            object? deploy = null,
            IEnumerable<object>? domains = null,
            IEnumerable<object>? mounts = null,
            IEnumerable<object>? ports = null,
            int memoryReservation = 0,
            int memoryLimit = 0,
            int cpuReservation = 0,
            int cpuLimit = 0,
            bool maintenanceEnabled = false,
            string? maintenanceTitle = null,
            string? maintenanceSubtitle = null,
            string? maintenanceCustomLogo = null,
            string? maintenanceCustomCss = null,
            bool maintenanceHideLogo = false,
            bool maintenanceHideLinks = false
        )
        {
            Dictionary<string, object> payload = new Dictionary<string, object>();

            payload["projectName"] = projectName;
            payload["serviceName"] = serviceName;

            if (!string.IsNullOrEmpty(sourceType) && !string.IsNullOrEmpty(sourceImage))
            {
                Dictionary<string, object>  source = new Dictionary<string, object>
                {
                    ["type"] = sourceType,
                    ["image"] = sourceImage,
                };

                if (!string.IsNullOrEmpty(sourceUsername))
                    source["username"] = sourceUsername;

                if (!string.IsNullOrEmpty(sourcePassword))
                    source["password"] = sourcePassword;

                payload["source"] = source;
            }

            if (!string.IsNullOrEmpty(buildType) && !string.IsNullOrEmpty(buildFile))
            {
                payload["build"] = new Dictionary<string, object>
                {
                    ["type"] = buildType,
                    ["file"] = buildFile,
                };
            }

            if (!string.IsNullOrEmpty(env))
            {
                payload["env"] = env;
            }

            if (basicAuth != null && basicAuth.Any())
            {
                payload["basicAuth"] = basicAuth
                    .Select(x => new { username = x.username, password = x.password })
                    .ToArray();
            }

            if (deploy != null)
            {
                payload["deploy"] = deploy;
            }

            if (domains != null && domains.Any())
            {
                payload["domains"] = domains.ToArray();
            }

            if (mounts != null && mounts.Any())
            {
                payload["mounts"] = mounts.ToArray();
            }

            if (ports != null && ports.Any())
            {
                payload["ports"] = ports.ToArray();
            }

            if (memoryReservation != 0 || memoryLimit != 0 || cpuReservation != 0 || cpuLimit != 0)
            {
                payload["resources"] = new
                {
                    memoryReservation,
                    memoryLimit,
                    cpuReservation,
                    cpuLimit,
                };
            }

            if (
                maintenanceEnabled
                || !string.IsNullOrEmpty(maintenanceTitle)
                || !string.IsNullOrEmpty(maintenanceSubtitle)
                || !string.IsNullOrEmpty(maintenanceCustomLogo)
                || !string.IsNullOrEmpty(maintenanceCustomCss)
                || maintenanceHideLogo
                || maintenanceHideLinks
            )
            {
                payload["maintenance"] = new
                {
                    enabled = maintenanceEnabled,
                    title = maintenanceTitle ?? string.Empty,
                    subtitle = maintenanceSubtitle ?? string.Empty,
                    customLogo = maintenanceCustomLogo ?? string.Empty,
                    customCss = maintenanceCustomCss ?? string.Empty,
                    hideLogo = maintenanceHideLogo,
                    hideLinks = maintenanceHideLinks,
                };
            }
            CreatePayloadTiedUp Tiedup = new CreatePayloadTiedUp { Json = payload };

            StringContent content = new StringContent(
                JsonSerializer.Serialize(Tiedup),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage response = await _client.PostAsync(
                "/api/trpc/services.app.createService",
                content
            );
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(
                    $"[Easypanel API] API call failed with status {response.StatusCode} | {errorContent}"
                );
            }

            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<SystemStats> GetSystemStatsAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(
                "/api/trpc/monitor.getSystemStats"
            );
            string json = await response.Content.ReadAsStringAsync();
            RootResponse<SystemStats>? rootResponse = JsonSerializer.Deserialize<RootResponse<SystemStats>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            SystemStats? stats = rootResponse?.Result?.Data?.Json;

            return stats
                ?? new SystemStats
                {
                    Uptime = 0,
                    MemInfo = new MemInfo
                    {
                        TotalMemMb = 0,
                        UsedMemMb = 0,
                        FreeMemMb = 0,
                        UsedMemPercentage = 0,
                        FreeMemPercentage = 0,
                    },
                    DiskInfo = new DiskInfo
                    {
                        TotalGb = "0",
                        UsedGb = "0",
                        FreeGb = "0",
                        UsedPercentage = "0",
                        FreePercentage = "0",
                    },
                    CpuInfo = new CpuInfo
                    {
                        UsedPercentage = 0,
                        Count = 0,
                        LoadAvg = Array.Empty<double>(),
                    },
                    Network = new NetworkInfo { InputMb = 0, OutputMb = 0 },
                };
        }
    }
}
