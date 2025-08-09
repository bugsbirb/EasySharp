public class User
{
    public required string Id { get; set; }

    public required DateTime CreatedAt { get; set; }

    public required string Email { get; set; }

    public required bool Admin { get; set; }

    public string? Password { get; set; }

    public required string ApiToken { get; set; }
}
