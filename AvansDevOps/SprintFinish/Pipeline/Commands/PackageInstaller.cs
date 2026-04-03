namespace AvansDevOps.SprintFinish.Pipeline.Commands;

public class PackageInstaller : ICommand
{

    public void RunCommand()
    {
        Console.WriteLine("Installing packages");
    }
    public void Execute()
    {
        RunCommand();
    }
}