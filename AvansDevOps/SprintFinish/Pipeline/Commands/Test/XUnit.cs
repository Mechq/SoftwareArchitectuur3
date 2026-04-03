namespace AvansDevOps.SprintFinish.Pipeline.Commands.Test;

public class XUnit : ITestStrategy
{
    private void RunTests()
    {
        Console.WriteLine("Running NUnit tests");
    }

    public void Execute()
    {
        RunTests();
    }
}