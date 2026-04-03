namespace AvansDevOps.SprintFinish.Pipeline;

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