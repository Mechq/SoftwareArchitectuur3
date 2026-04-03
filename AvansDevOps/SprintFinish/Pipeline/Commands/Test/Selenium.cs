namespace AvansDevOps.SprintFinish.Pipeline.Commands.Test;

public class Selenium : ITestStrategy
{
    private void RunTests()
    {
        Console.WriteLine("Running Selenium tests");
    }

    public void Execute()
    {
        RunTests();
    }
}