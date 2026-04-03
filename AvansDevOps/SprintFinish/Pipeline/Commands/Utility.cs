namespace AvansDevOps.SprintFinish.Pipeline;

public class Utility : ICommand
{
    public void Execute()
    {
        Console.WriteLine($"{nameof(Utility)} executed successfully.");
    }
}