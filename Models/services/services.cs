using System.Text.Json.Serialization;
public class Root
{
    public List<Service>? services { get; set; }
}

public class Service
{
    public string? projectName { get; set; }
    public string? name { get; set; }
    public string? type { get; set; }
    public bool? enabled { get; set; }
    public string? token { get; set; }
    public object? primaryDomainId { get; set; }
    public string? env { get; set; }
    public Deploy? deploy { get; set; }
    public List<object>? mounts { get; set; }
    public List<object>? ports { get; set; }
    public ServiceSource? source { get; set; }
    public ServiceBuild? build { get; set; }
    public object? createDotEnv { get; set; }
    public Commit? commit { get; set; }
}


public class PayloadWrapper<T>
{
    public required T json { get; set; }
}
public class ServicePayload
{
    public required string projectName { get; set; }
    public required string serviceName { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? forceRebuild { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? notes { get; set; }
}

public partial class Deploy
{
    public int? replicas { get; set; }
    public object? command { get; set; }
    public bool? zeroDowntime { get; set; }
}

public partial class ServiceSource
{
    public string? type { get; set; }
    public string? owner { get; set; }
    public string? repo { get; set; }
    public string? @ref { get; set; }
    public string? path { get; set; }
    public bool? autoDeploy { get; set; }
}

public partial class ServiceBuild
{
    public string? type { get; set; }
    public string? nixpacksVersion { get; set; }
}

public class Commit
{
    public string? sha { get; set; }
    public string? node_id { get; set; }
    public CommitDetails? commit { get; set; }
    public string? url { get; set; }
    public string? html_url { get; set; }
    public string? comments_url { get; set; }
    public Author? author { get; set; }
    public Committer? committer { get; set; }
    public List<Parent>? parents { get; set; }
}

public class CommitDetails
{
    public Author? author { get; set; }
    public Committer? committer { get; set; }
    public string? message { get; set; }
    public Tree? tree { get; set; }
    public string? url { get; set; }
    public int? comment_count { get; set; }
    public Verification? verification { get; set; }
}

public class Author
{
    public string? login { get; set; }

    public int? id { get; set; }

    public string? node_id { get; set; }

    public string? avatar_url { get; set; }

    public string? gravatar_id { get; set; }

    public string? url { get; set; }

    public string? html_url { get; set; }

    public string? followers_url { get; set; }

    public string? following_url { get; set; }

    public string? gists_url { get; set; }

    public string? starred_url { get; set; }

    public string? subscriptions_url { get; set; }

    public string? organizations_url { get; set; }

    public string? repos_url { get; set; }

    public string? events_url { get; set; }

    public string? received_events_url { get; set; }

    public string? type { get; set; }

    public bool? site_admin { get; set; }
}

public class Committer : Author { }

public class Tree
{
    public required string sha { get; set; }
    public required string url { get; set; }
}

public class Verification
{
    public required bool verified { get; set; }
    public required string reason { get; set; }
    public required string signature { get; set; }
    public required string payload { get; set; }
    public required DateTime? verified_at { get; set; }
}

public class Parent
{
    public required string sha { get; set; }
    public required string url { get; set; }
    public required string html_url { get; set; }
}
