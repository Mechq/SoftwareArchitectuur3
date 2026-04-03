using AvansDevOps.Notification;
using AvansDevOps.SprintFinish.Pipeline;

namespace AvansDevOps.SprintFinish;

public class FeedbackSprintStrategy : ISprintStrategy
{
    private bool _hasUploadedSummary = false;
    private INotification _notifier;
    private readonly IPipelineToolFactory _factory;

    public FeedbackSprintStrategy(INotification notifier, IPipelineToolFactory factory)
    {
        _notifier = notifier;
        _factory = factory;
    }

    public bool IsFinished()
    {
        return _hasUploadedSummary;
    }
    
    public void UploadedSummary()
    {
        Console.WriteLine("Uploading summary to server...");
        _hasUploadedSummary = true;
        Console.WriteLine("Uploaded summary to server.");
    }

    public void FinishSprint()
    {
        if (_hasUploadedSummary)
        {
            Console.WriteLine("Finishing sprint.");
            _notifier.SendNotification("Sprint finished.");
        }
        else
        {
            Console.WriteLine("You need to upload a summary to finish this sprint");
        }
    }
    
    public bool StartPipeline(PipelineHandler handler)
    {
        Console.WriteLine("Running feedback sprint...");
        return handler.StartPipeline();
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
}