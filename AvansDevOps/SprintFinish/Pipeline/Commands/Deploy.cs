namespace AvansDevOps.SprintFinish.Pipeline;

public class Deploy : ICommand
{
    private void DeployToAzure()
    {
        Console.WriteLine("Deploying to Azure");
    }
    public void Execute()
    {
        DeployToAzure();
    }
}