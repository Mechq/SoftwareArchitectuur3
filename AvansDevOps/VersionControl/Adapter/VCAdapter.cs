namespace AvansDevOps.VersionControl;

public abstract class VCAdapter : IVersionControl
{
    protected readonly IVCService Adaptee;

    protected VCAdapter(IVCService ivcService)
    {
        Adaptee = ivcService;
    }
    public abstract void SendCommand(String info);
}