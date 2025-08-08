public class SystemStats
{
    public required double Uptime { get; set; }

    public required MemInfo MemInfo { get; set; }

    public required DiskInfo DiskInfo { get; set; }

    public required CpuInfo CpuInfo { get; set; }

    public required NetworkInfo Network { get; set; }
}

public class MemInfo
{
    public required double TotalMemMb { get; set; }

    public required double UsedMemMb { get; set; }

    public required double FreeMemMb { get; set; }

    public required double UsedMemPercentage { get; set; }

    public required double FreeMemPercentage { get; set; }
}

public class DiskInfo
{
    public required string TotalGb { get; set; }

    public required string UsedGb { get; set; }

    public required string FreeGb { get; set; }

    public required string UsedPercentage { get; set; }

    public required string FreePercentage { get; set; }
}

public class CpuInfo
{
    public required double UsedPercentage { get; set; }

    public required int Count { get; set; }

    public required double[] LoadAvg { get; set; }
}

public class NetworkInfo
{
    public required double InputMb { get; set; }

    public required double OutputMb { get; set; }
}
