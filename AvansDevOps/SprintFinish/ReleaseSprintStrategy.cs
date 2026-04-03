using AvansDevOps.Notification;
using AvansDevOps.SprintFinish.Pipeline;

namespace AvansDevOps.SprintFinish;

public class ReleaseSprintStrategy : ISprintStrategy
{
    private INotification _notifier;
    private readonly IPipelineToolFactory _factory;
    private bool _isReleased;

    public ReleaseSprintStrategy(INotification notifier,  IPipelineToolFactory factory)
    {
        _notifier = notifier;
        _factory = factory;
        
    }

    public bool IsFinished()
    {
        return _isReleased;
    }
    
    public bool StartPipeline(PipelineHandler handler)
    {
        Console.WriteLine("Running release sprint...");
        _isReleased = handler.StartPipeline();
        return _isReleased;
    }
    
    public Pipeline.Pipeline BuildPipeline()
    {
        return new PipelineBuilder(_factory)
            .AddSourceStep()
            .AddPackageStep()
            .AddBuildStep()
            .AddTestStep()
            .AddAnalyseStep()
            .GetResult(); // no deploy action
    }

    public void FinishSprint()
    {
        
    }
}