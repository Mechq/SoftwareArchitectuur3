namespace AvansDevOps.SprintFinish.Pipeline.Test;

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