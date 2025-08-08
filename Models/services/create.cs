using System.Text.Json.Serialization;


public class Success
{
    public required bool success { get; set; }
}

public class CreateServiceRequest
{
    public required CreateProject Json { get; set; }
}


public partial class CreateProject
{
    [JsonPropertyName("projectName")]
    public required string ProjectName { get; set; }

    [JsonPropertyName("serviceName")]
    public required string ServiceName { get; set; }

    [JsonPropertyName("source")]
    public CreateSource? Source { get; set; }

    [JsonPropertyName("build")]
    public CreateBuild? Build { get; set; }

    [JsonPropertyName("env")]
    public string? Env { get; set; }

    [JsonPropertyName("basicAuth")]
    public BasicAuth[]? BasicAuth { get; set; }

    [JsonPropertyName("deploy")]
    public Deploy? Deploy { get; set; }

    [JsonPropertyName("domains")]
    public object[]? Domains { get; set; }

    [JsonPropertyName("mounts")]
    public object[]? Mounts { get; set; }

    [JsonPropertyName("ports")]
    public object[]? Ports { get; set; }

    [JsonPropertyName("resources")]
    public Resources? Resources { get; set; }

    [JsonPropertyName("maintenance")]
    public Maintenance? Maintenance { get; set; }
}

public class Domain

{
    public string Host { get; set; } = "";
}

public partial class BasicAuth
{
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }
}

public partial class CreateBuild
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("file")]
    public string? File { get; set; }
}

public partial class Deploy { }

public partial class Maintenance
{
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("subtitle")]
    public string? Subtitle { get; set; }

    [JsonPropertyName("customLogo")]
    public string? CustomLogo { get; set; }

    [JsonPropertyName("customCss")]
    public string? CustomCss { get; set; }

    [JsonPropertyName("hideLogo")]
    public bool HideLogo { get; set; }

    [JsonPropertyName("hideLinks")]
    public bool HideLinks { get; set; }
}

public partial class Resources
{
    [JsonPropertyName("memoryReservation")]
    public long MemoryReservation { get; set; }

    [JsonPropertyName("memoryLimit")]
    public long MemoryLimit { get; set; }

    [JsonPropertyName("cpuReservation")]
    public long CpuReservation { get; set; }

    [JsonPropertyName("cpuLimit")]
    public long CpuLimit { get; set; }
}

public partial class CreateSource
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("image")]
    public string? Image { get; set; }

    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }
}
