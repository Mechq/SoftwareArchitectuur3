namespace AvansDevOps.VersionControl.Service;

public class JenkinsService : IVCService
{
    public void SendCommand(string command)
    {
        Console.WriteLine("Sending command to Jenkins... \"" + command + "\"");
    }
}