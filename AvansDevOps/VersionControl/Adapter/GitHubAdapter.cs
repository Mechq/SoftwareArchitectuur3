namespace AvansDevOps.VersionControl;

public class GitHubAdapter : VCAdapter
{
    
    public GitHubAdapter(IVCService service) : base(service) {}

    public override void SendCommand(string command)
    {
        Adaptee.SendCommand(command);
    }
}