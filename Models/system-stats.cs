using System.Text.Json.Serialization;
/// <summary>
/// Easypanel monitor stats
/// </summary>
public class SystemStats
{
    /// <summary>
    /// How long the panel has been up for
    /// </summary>
    public required double uptime { get; set; }

    /// <summary>
    ///  Memory statistics
    /// </summary>

    [JsonPropertyName("MemInfo")]
    public required MemInfo Memory { get; set; }
     /// <summary>
    ///  Disk statistics
    /// </summary>
    [JsonPropertyName("diskInfo")]
    public required DiskInfo Disk { get; set; }


}

/// <summary>
/// Memory information for system stats
/// </summary>
public class MemInfo
{
    /// <summary>
    /// The amount of memory that can be used.
    /// </summary>
    [JsonPropertyName("totalMemMb")]
    public double TotalMemoryMb { get; set; }
    /// <summary>
    /// The amount of memory that is being used.
    /// </summary>
    [JsonPropertyName("usedMemMb")]
    public double UsedMemoryMb { get; set; }
    /// <summary>
    /// The amount of memory that is free to be used.
    /// </summary>
    [JsonPropertyName("freeMemMb")]
    public double FreeMemoryMb { get; set; }
    /// <summary>
    /// The percentage of memory being used.
    /// </summary>
    [JsonPropertyName("usedMemPercentage")]
    public double UsedMemoryPercentage { get; set; }
    /// <summary>
    /// The percentage of memory not being used.
    /// </summary>

    [JsonPropertyName("freeMemPercentage")]
    public double FreeMemoryPercentage { get; set; }
}


/// <summary>
/// Disk information for system stats
/// </summary>
public class DiskInfo
{
    /// <summary>
    /// The total amount of gigabytes avaliable
    /// </summary>
    public required string totalGb { get; set; }
    /// <summary>
    /// The total amount of gigabytes being used.
    /// </summary>
    public required string usedGb { get; set; }
    /// <summary>
    /// The total amount of gigabytes that is free to use.
    /// </summary>
    public required string freeGb { get; set; }
    /// <summary>
    /// The percentage of gigabytes that is being used.
    /// </summary>
    public required string usedPercentage { get; set; }
    /// <summary>
    /// The percentage of gigabytes that is free to use.
    /// </summary>
    public required string freePercentage { get; set; }

}