using System.Text.Json;
using System.Text.Json.Serialization;

namespace EasySharp
{
    /// <summary>
    /// A wrapper class to interact with the Easypanel API.
    /// </summary>
    public class Easypanel
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Initializes a new instance of <see cref="Easypanel"/>.
        /// </summary>
        /// <param name="baseUrl">The base URL of the API.</param>
        /// <param name="apiKey">Optional API key for authorization header.</param>
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

        /// <summary>
        /// Asynchronously fetches the list of projects from the API.
        /// </summary>
        /// <returns>A list of <see cref="Project"/> instances.</returns>
        /// <exception cref="HttpRequestException">Thrown when the HTTP response indicates failure.</exception>
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

        /// <summary>
        /// Asynchronously fetches the list of services from the API.
        /// </summary>
        /// <returns>A list of <see cref="Service"/> instances.</returns>
        /// <exception cref="HttpRequestException">Thrown when the HTTP response indicates failure.</exception>
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

        /// <summary>
        /// Gets the current user asynchronously.
        /// </summary>
        /// <returns>The <see cref="User"/> object if found; otherwise, a default empty user.</returns>
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

        /// <summary>
        /// Gets the current system stats asynchronously.
        /// </summary>
        /// <returns>
        /// The <see cref="SystemStats"/> object if found; ofthwerise, a defaulty empty stat </returns>
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
