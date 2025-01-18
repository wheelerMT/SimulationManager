namespace AutoRunner.Core;

public class SystemInfo
{
    public SystemInfo()
    {
        AvailableCores = Environment.ProcessorCount;
        CoresToUse = AvailableCores / 4;
        User = Environment.UserName;
    }

    private int AvailableCores { get; }
    public int CoresToUse { get; set; }

    public string User { get; }
}