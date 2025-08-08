using System.Text.Json.Serialization;

public class RootResponse<T>
{
    [JsonPropertyName("result")]
    public Result<T>? Result { get; set; }
}

public class Result<T>
{
    [JsonPropertyName("data")]
    public Data<T>? Data { get; set; }
}

public class Data<T>
{
    [JsonPropertyName("json")]
    public T? Json { get; set; }
}
