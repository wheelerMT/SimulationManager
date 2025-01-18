namespace AutoRunner.Core;

public class SystemInfo
{
    public SystemInfo()
    {
        AvailableCores = Environment.ProcessorCount;
        CoresToUse = AvailableCores / 4;
    }

    private int AvailableCores { get; }
    public int CoresToUse { get; set; }
}