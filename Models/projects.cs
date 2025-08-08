/// <summary>
/// Represents a project with a name and creation date.
/// </summary>
public class Project
{
    /// <summary>
    /// The name of the project.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The date the project was created.
/// </summary>
    public DateTime CreatedAt { get; set; }
}


/// <summary>
/// Represents a container for a list of projects.
/// </summary>
public class JsonData
{
    /// <summary>
    /// The list of projects returned by the API.
/// </summary>
    public required List<Project> Projects { get; set; }
}
