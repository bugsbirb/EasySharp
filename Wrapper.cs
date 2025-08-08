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
            HttpResponseMessage response = await _client.GetAsync("/api/trpc/projects.listProjectsAndServices");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            var rootResponse = JsonSerializer.Deserialize<RootResponse<JsonData>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return rootResponse?.Result?.Data?.Json?.Projects ?? new List<Project>();
        }


        public async Task<List<Service>> GetServicesAsync()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/trpc/projects.listProjectsAndServices");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var rootResponse = JsonSerializer.Deserialize<RootResponse<Root>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }
            );
            return rootResponse?.Result?.Data?.Json?.services ?? new List<Service>();
        }

        public async Task<User> GetUserAsync()
        {
            HttpResponseMessage response = await _client.GetAsync("api/trpc/auth.getUser");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            var rootResponse = JsonSerializer.Deserialize<RootResponse<User>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return rootResponse?.Result?.Data?.Json ?? new User
            {
                Id = string.Empty,
                CreatedAt = DateTime.MinValue,
                Email = string.Empty,
                Admin = false,
                ApiToken = string.Empty
            };
        }
        public async Task<Success> CreateServiceAsync(
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
            var payload = new Dictionary<string, object>();

            payload["projectName"] = projectName;
            payload["serviceName"] = serviceName;

            if (!string.IsNullOrEmpty(sourceType) && !string.IsNullOrEmpty(sourceImage))
            {
                var source = new Dictionary<string, object>
                {
                    ["type"] = sourceType,
                    ["image"] = sourceImage
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
                    ["file"] = buildFile
                };
            }

            if (!string.IsNullOrEmpty(env))
            {
                payload["env"] = env;
            }

            if (basicAuth != null && basicAuth.Any())
            {
                payload["basicAuth"] = basicAuth.Select(x => new { username = x.username, password = x.password }).ToArray();
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
                    cpuLimit
                };
            }

            if (maintenanceEnabled ||
                !string.IsNullOrEmpty(maintenanceTitle) ||
                !string.IsNullOrEmpty(maintenanceSubtitle) ||
                !string.IsNullOrEmpty(maintenanceCustomLogo) ||
                !string.IsNullOrEmpty(maintenanceCustomCss) ||
                maintenanceHideLogo ||
                maintenanceHideLinks)
            {
                payload["maintenance"] = new
                {
                    enabled = maintenanceEnabled,
                    title = maintenanceTitle ?? string.Empty,
                    subtitle = maintenanceSubtitle ?? string.Empty,
                    customLogo = maintenanceCustomLogo ?? string.Empty,
                    customCss = maintenanceCustomCss ?? string.Empty,
                    hideLogo = maintenanceHideLogo,
                    hideLinks = maintenanceHideLinks
                };
            }
            var Tiedup = new { json = payload };
            var content = new StringContent(
                JsonSerializer.Serialize(Tiedup),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage response = await _client.PostAsync("/api/trpc/services.app.createService", content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API call failed with status {response.StatusCode}: {errorContent}");
            }

            response.EnsureSuccessStatusCode();


            return new Success { success = true };
        }


        public async Task<SystemStats> GetSystemStatsAsync()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/trpc/monitor.getSystemStats");
            string json = await response.Content.ReadAsStringAsync();
            var rootResponse = JsonSerializer.Deserialize<RootResponse<SystemStats>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            var stats = rootResponse?.Result?.Data?.Json;

            return stats ?? new SystemStats
            {
                Uptime = 0,
                MemInfo = new MemInfo
                {
                    TotalMemMb = 0,
                    UsedMemMb = 0,
                    FreeMemMb = 0,
                    UsedMemPercentage = 0,
                    FreeMemPercentage = 0
                },
                DiskInfo = new DiskInfo
                {
                    TotalGb = "0",
                    UsedGb = "0",
                    FreeGb = "0",
                    UsedPercentage = "0",
                    FreePercentage = "0"
                },
                CpuInfo = new CpuInfo
                {
                    UsedPercentage = 0,
                    Count = 0,
                    LoadAvg = Array.Empty<double>()
                },
                Network = new NetworkInfo
                {
                    InputMb = 0,
                    OutputMb = 0
                }
            };
        }







    }
}
