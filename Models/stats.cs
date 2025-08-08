/// <summary>
/// Represents overall system statistics including uptime, memory, disk, CPU, and network information.
/// </summary>
public class SystemStats
{
    /// <summary>
    /// The amount of time (in seconds) the system has been running.
    /// </summary>
    public required double Uptime { get; set; }

    /// <summary>
    /// Memory usage details of the system.
    /// </summary>
    public required MemInfo MemInfo { get; set; }

    /// <summary>
    /// Disk usage details of the system.
    /// </summary>
    public required DiskInfo DiskInfo { get; set; }

    /// <summary>
    /// CPU usage and load average information.
    /// </summary>
    public required CpuInfo CpuInfo { get; set; }

    /// <summary>
    /// Network input and output statistics.
    /// </summary>
    public required NetworkInfo Network { get; set; }
}

/// <summary>
/// Memory usage information.
/// </summary>
public class MemInfo
{
    /// <summary>
    /// Total memory available in megabytes.
    /// </summary>
    public required double TotalMemMb { get; set; }

    /// <summary>
    /// Memory currently used in megabytes.
    /// </summary>
    public required double UsedMemMb { get; set; }

    /// <summary>
    /// Free memory available in megabytes.
    /// </summary>
    public required double FreeMemMb { get; set; }

    /// <summary>
    /// Percentage of memory currently used.
    /// </summary>
    public required double UsedMemPercentage { get; set; }

    /// <summary>
    /// Percentage of free memory available.
    /// </summary>
    public required double FreeMemPercentage { get; set; }
}

/// <summary>
/// Disk usage information.
/// </summary>
public class DiskInfo
{
    /// <summary>
    /// Total disk size in gigabytes.
    /// </summary>
    public required string TotalGb { get; set; }

    /// <summary>
    /// Disk space currently used in gigabytes.
    /// </summary>
    public required string UsedGb { get; set; }

    /// <summary>
    /// Free disk space available in gigabytes.
    /// </summary>
    public required string FreeGb { get; set; }

    /// <summary>
    /// Percentage of disk space used.
    /// </summary>
    public required string UsedPercentage { get; set; }

    /// <summary>
    /// Percentage of disk space free.
    /// </summary>
    public required string FreePercentage { get; set; }
}

/// <summary>
/// CPU usage information including load averages.
/// </summary>
public class CpuInfo
{
    /// <summary>
    /// Percentage of CPU currently in use.
    /// </summary>
    public required double UsedPercentage { get; set; }

    /// <summary>
    /// Number of CPU cores.
    /// </summary>
    public required int Count { get; set; }

    /// <summary>
    /// Array representing the CPU load average over 1, 5, and 15 minutes.
    /// </summary>
    public required double[] LoadAvg { get; set; }
}

/// <summary>
/// Network input/output statistics.
/// </summary>
public class NetworkInfo
{
    /// <summary>
    /// Amount of incoming network data in megabytes.
    /// </summary>
    public required double InputMb { get; set; }

    /// <summary>
    /// Amount of outgoing network data in megabytes.
    /// </summary>
    public required double OutputMb { get; set; }
}
