namespace AvansDevOps.SprintFinish.Pipeline.Commands.Build;

public class Maven : IBuildStrategy
{
    private void Build()
    {
        Console.WriteLine("Building Maven");
    }
    
    public void Execute()
    {
        Build();
    }
}