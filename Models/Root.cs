/// <summary>
/// Represents the root response wrapper for the API response.
/// </summary>
/// <typeparam name="T">The type of the JSON payload inside the response.</typeparam>
public class RootResponse<T>
{
    /// <summary>
    /// The result of the API call.
    /// </summary>
    public required Result<T> Result { get; set; }
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
    public required Data<T> Data { get; set; }
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
    public required T Json { get; set; }
}