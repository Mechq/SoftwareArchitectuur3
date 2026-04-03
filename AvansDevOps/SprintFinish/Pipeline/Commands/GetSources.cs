namespace AvansDevOps.SprintFinish.Pipeline.Commands;

public class GetSources : ICommand
{
    public void Execute()
    {
        GetSource();
    }

    private void GetSource()
    {
        Console.WriteLine("GetSources");
    }
}