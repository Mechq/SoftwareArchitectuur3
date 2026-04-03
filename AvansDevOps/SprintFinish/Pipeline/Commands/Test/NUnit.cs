namespace AvansDevOps.SprintFinish.Pipeline.Test;

public class NUnit : ITestStrategy
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