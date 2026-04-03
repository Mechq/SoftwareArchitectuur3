namespace AvansDevOps.SprintFinish.Pipeline.Commands;

public class Utility : ICommand
{
    public void Execute()
    {
        Console.WriteLine($"{nameof(Utility)} executed successfully.");
    }
} 