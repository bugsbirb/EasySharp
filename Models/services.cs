/// <summary>
/// Represents the root object containing a list of services.
/// </summary>
public class Root
{
    /// <summary>
    /// List of services contained in the root configuration.
    /// </summary>
    public List<Service>? services { get; set; }
}

/// <summary>
/// Represents a service configuration within a project.
/// </summary>
public class Service
{
    /// <summary>
    /// The name the service is in.
    /// </summary>
    public string? projectName { get; set; }
    /// <summary>
    /// The name of the service.
    /// </summary>
    public string? name { get; set; }
    /// <summary>
    /// What type of service this is (app, compose, wordpress etc).
    /// </summary>
    public string? type { get; set; }
    /// <summary>
    /// Whether the service is enabled or not.
    /// </summary>
    public bool? enabled { get; set; }
    /// <summary>
    /// The token of the project.
    /// </summary>
    public string? token { get; set; }
    /// <summary>
    /// The ID of the primary domain, if any (nullable).
    /// </summary>
    public object? primaryDomainId { get; set; }
    /// <summary>
    /// Environment variables string for the service.
    /// </summary>
    public string? env { get; set; }
    /// <summary>
    /// Deployment configuration details.
    /// </summary>
    public Deploy? deploy { get; set; }
    /// <summary>
    /// Storage mount points for the service.
    /// </summary>
    public List<object>? mounts { get; set; }
    /// <summary>
    /// Ports that the service exposes or listens on.
    /// </summary>
    public List<object>? ports { get; set; }
    /// <summary>
    /// Source repository and branch details for building the service.
    /// </summary>
    public Source? source { get; set; }
    /// <summary>
    /// Build configuration details.
    /// </summary>
    public Build? build { get; set; }
    /// <summary>
    /// Unknown purpose; possibly controls creation of a .env file.
    /// </summary>
    public object? createDotEnv { get; set; }
    /// <summary>
    /// Information about the latest commit deployed to the service.
    /// </summary>
    public Commit? commit { get; set; }
}

/// <summary>
/// Deployment configuration details for a service.
/// </summary>
public class Deploy
{
    /// <summary>
    /// Number of replicas to deploy.
    /// </summary>
    public int? replicas { get; set; }
    /// <summary>
    /// Command to run during deployment, if any.
    /// </summary>
    public object? command { get; set; }
    /// <summary>
    /// Whether zero downtime deployment is enabled.
    /// </summary>
    public bool? zeroDowntime { get; set; }
}

/// <summary>
/// Source control details related to the service's code repository.
/// </summary>
public class Source
{
    /// <summary>
    /// Type of source (e.g., git).
    /// </summary>
    public string? type { get; set; }
    /// <summary>
    /// Owner of the source repository.
    /// </summary>
    public string? owner { get; set; }
    /// <summary>
    /// Repository name.
    /// </summary>
    public string? repo { get; set; }
    /// <summary>
    /// Git reference (branch or tag).
    /// </summary>
    public string? @ref { get; set; }
    /// <summary>
    /// Path within the repository to use.
    /// </summary>
    public string? path { get; set; }
    /// <summary>
    /// Whether the service auto-deploys from this source.
    /// </summary>
    public bool? autoDeploy { get; set; }
}

/// <summary>
/// Build configuration details for a service.
/// </summary>
public class Build
{
    /// <summary>
    /// Build type or system used.
    /// </summary>
    public string? type { get; set; }
    /// <summary>
    /// Version of the nixpacks builder used.
    /// </summary>
    public string? nixpacksVersion { get; set; }
}

/// <summary>
/// Represents the commit information associated with the service deployment.
/// </summary>
public class Commit
{
    /// <summary>
    /// SHA hash of the commit.
    /// </summary>
    public string? sha { get; set; }
    /// <summary>
    /// Node ID of the commit.
    /// </summary>
    public string? node_id { get; set; }
    /// <summary>
    /// Detailed commit information.
    /// </summary>
    public CommitDetails? commit { get; set; }
    /// <summary>
    /// API URL for the commit.
    /// </summary>
    public string? url { get; set; }
    /// <summary>
    /// HTML URL for viewing the commit on GitHub.
    /// </summary>
    public string? html_url { get; set; }
    /// <summary>
    /// URL for comments on the commit.
    /// </summary>
    public string? comments_url { get; set; }
    /// <summary>
    /// Author details of the commit.
    /// </summary>
    public Author? author { get; set; }
    /// <summary>
    /// Committer details of the commit.
    /// </summary>
    public Committer? committer { get; set; }
    /// <summary>
    /// List of parent commits.
    /// </summary>
    public List<Parent>? parents { get; set; }
}

/// <summary>
/// Details about the commit itself, including author, message, and verification.
/// </summary>
public class CommitDetails
{
    /// <summary>
    /// Author information for the commit.
    /// </summary>
    public Author? author { get; set; }
    /// <summary>
    /// Committer information for the commit.
    /// </summary>
    public Committer? committer { get; set; }
    /// <summary>
    /// Commit message.
    /// </summary>
    public string? message { get; set; }
    /// <summary>
    /// Tree object related to the commit.
    /// </summary>
    public Tree? tree { get; set; }
    /// <summary>
    /// API URL for the commit details.
    /// </summary>
    public string? url { get; set; }
    /// <summary>
    /// Number of comments on the commit.
    /// </summary>
    public int? comment_count { get; set; }
    /// <summary>
    /// Verification details of the commit signature.
    /// </summary>
    public Verification? verification { get; set; }
}

/// <summary>
/// Represents an author or user in the API response.
/// </summary>
public class Author
{
    /// <summary>
    /// The username/login of the author.
    /// </summary>
    public string? login { get; set; }

    /// <summary>
    /// The unique ID of the author.
    /// </summary>
    public int? id { get; set; }

    /// <summary>
    /// The node ID of the author (usually a string identifier).
    /// </summary>
    public string? node_id { get; set; }

    /// <summary>
    /// URL to the author's avatar image.
    /// </summary>
    public string? avatar_url { get; set; }

    /// <summary>
    /// The gravatar ID, if any (can be empty).
    /// </summary>
    public string? gravatar_id { get; set; }

    /// <summary>
    /// The URL to the author's API resource.
    /// </summary>
    public string? url { get; set; }

    /// <summary>
    /// The URL to the author's HTML page.
    /// </summary>
    public string? html_url { get; set; }

    /// <summary>
    /// URL to the author's followers API endpoint.
    /// </summary>
    public string? followers_url { get; set; }

    /// <summary>
    /// URL to the author's following API endpoint.
    /// </summary>
    public string? following_url { get; set; }

    /// <summary>
    /// URL to the author's gists API endpoint.
    /// </summary>
    public string? gists_url { get; set; }

    /// <summary>
    /// URL to the author's starred API endpoint.
    /// </summary>
    public string? starred_url { get; set; }

    /// <summary>
    /// URL to the author's subscriptions API endpoint.
    /// </summary>
    public string? subscriptions_url { get; set; }

    /// <summary>
    /// URL to the author's organizations API endpoint.
    /// </summary>
    public string? organizations_url { get; set; }

    /// <summary>
    /// URL to the author's repositories API endpoint.
    /// </summary>
    public string? repos_url { get; set; }

    /// <summary>
    /// URL to the author's events API endpoint.
    /// </summary>
    public string? events_url { get; set; }

    /// <summary>
    /// URL to the author's received events API endpoint.
    /// </summary>
    public string? received_events_url { get; set; }

    /// <summary>
    /// The type of the user (e.g., "User", "Organization").
    /// </summary>
    public string? type { get; set; }

    /// <summary>
    /// Whether the user is a site administrator.
    /// </summary>
    public bool? site_admin { get; set; }
}

/// <summary>
/// Represents the committer of a commit (inherits author properties).
/// </summary>
public class Committer : Author { }

/// <summary>
/// Represents the git tree object associated with a commit.
/// </summary>
public class Tree
{
    /// <summary>
    /// SHA hash of the tree object.
    /// </summary>
    public required string sha { get; set; }
    /// <summary>
    /// URL to the tree object.
    /// </summary>
    public required string url { get; set; }
}

/// <summary>
/// Verification details of the commit.
/// </summary>
public class Verification
{
    /// <summary>
    /// Indicates whether the commit signature is verified.
    /// </summary>
    public required bool verified { get; set; }
    /// <summary>
    /// Reason for verification status.
    /// </summary>
    public required string reason { get; set; }
    /// <summary>
    /// Signature string from commit verification.
    /// </summary>
    public required string signature { get; set; }
    /// <summary>
    /// Payload used for signature verification.
    /// </summary>
    public required string payload { get; set; }
    /// <summary>
    /// Date and time the verification was done.
    /// </summary>
    public required DateTime? verified_at { get; set; }
}

/// <summary>
/// Represents a parent commit in the commit history.
/// </summary>
public class Parent
{
    /// <summary>
    /// SHA hash of the parent commit.
    /// </summary>
    public required string sha { get; set; }
    /// <summary>
    /// API URL for the parent commit.
    /// </summary>
    public required string url { get; set; }
    /// <summary>
    /// HTML URL to view the parent commit on GitHub.
    /// </summary>
    public required string html_url { get; set; }
}
