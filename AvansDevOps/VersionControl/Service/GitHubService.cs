namespace AvansDevOps.VersionControl;

public class GitHubService : IVCService
{
    public void SendCommand(string command)
    {
        Console.WriteLine("Sending command to GitHub... \"" + command + "\"");
    }
}