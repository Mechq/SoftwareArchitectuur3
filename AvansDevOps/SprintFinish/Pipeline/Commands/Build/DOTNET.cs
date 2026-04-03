namespace AvansDevOps.SprintFinish.Pipeline.Commands.Build;

public class DOTNET : IBuildStrategy
{
    private void Build()
    {
        Console.WriteLine("Building DOTNET");
    }

    public void Execute()
    {
        Build();
    }
}