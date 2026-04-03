namespace AvansDevOps.SprintFinish.Pipeline;

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