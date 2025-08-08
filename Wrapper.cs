using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

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

        


    }
}
