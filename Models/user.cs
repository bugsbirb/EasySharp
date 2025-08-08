


/// <summary>
/// Represents a user in the system.
/// </summary>
public class User
{   /// <summary>
    /// The user's ID.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// When the user was created.
    /// </summary>
    public required DateTime CreatedAt { get; set; }

    /// <summary>
    /// The user's email address.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Whether the user is an admin or not.
    /// </summary>
    public required bool Admin { get; set; }

    /// <summary>
    /// The user's password. (Bit confused why that's even a thing.)
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// The user's API Token. (Why? Idk.)
    /// </summary>
    public required string ApiToken { get; set; }



}


