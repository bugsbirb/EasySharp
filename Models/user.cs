using System.Text.Json.Serialization;


public class User
{
    public required string Id { get; set; }

    public required DateTime CreatedAt { get; set; }

    public required string Email { get; set; }

    public required bool Admin { get; set; }

    public string? Password { get; set; }

    public required string ApiToken { get; set; }
}


public class UserPayload
{
    public required string email { get; set; }
    public required string password { get; set; }
    public bool admin { get; set; } 
}

public class UserPayloadTiedUp
{
    [JsonPropertyName("json")]
    public required UserPayload json { get; set; }
}