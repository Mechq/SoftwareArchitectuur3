namespace AvansDevOps.SprintFinish.Pipeline;

public class PipelineHandler
{
    private readonly Pipeline _pipeline;

    public PipelineHandler(Pipeline pipeline)
    {
        _pipeline = pipeline;
    }

    public bool StartPipeline()
    {
        Console.WriteLine("Pipeline started...");
        bool isSuccess = _pipeline.Execute();
        PrintLogs();
        return isSuccess;
    }

    public bool RedoPipeline()
    {
        Console.WriteLine("Redoing pipeline...");
        _pipeline.Execute();
        bool isSuccess = _pipeline.Execute();
        PrintLogs();
        return isSuccess;

    }

    private void PrintLogs()
    {
        foreach (var log in _pipeline.GetLogs())
            log.Print();
    }
}