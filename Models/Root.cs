using System.Text.Json.Serialization;

/// <summary>
/// Represents the root response wrapper for the API response.
/// </summary>
/// <typeparam name="T">The type of the JSON payload inside the response.</typeparam>
public class RootResponse<T>
{
    /// <summary>
    /// The result of the API call.
    /// </summary>
    [JsonPropertyName("result")]
    public Result<T>? Result { get; set; }
}

/// <summary>
/// Represents the result part of the API response.
/// </summary>
/// <typeparam name="T">The type of the JSON payload inside the result.</typeparam>
public class Result<T>
{
    /// <summary>
    /// The data container holding the JSON payload.
    /// </summary>
    [JsonPropertyName("data")]
    public Data<T>? Data { get; set; }
}

/// <summary>
/// Represents the data part of the API response.
/// </summary>
/// <typeparam name="T">The type of the JSON payload inside the data.</typeparam>
public class Data<T>
{
    /// <summary>
    /// The actual JSON payload, varies by API endpoint.
    /// </summary>
    [JsonPropertyName("json")]
    public T? Json { get; set; }
}
