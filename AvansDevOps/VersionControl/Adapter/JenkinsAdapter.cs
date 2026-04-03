namespace AvansDevOps.VersionControl.Adapter;

public class JenkinsAdapter :  VCAdapter
{
    public JenkinsAdapter(IVCService service) : base(service) {}

    public override void SendCommand(string command)
    {
        Adaptee.SendCommand(command);
    }
}