using AvansDevOps.Notification;
using AvansDevOps.SprintFinish;
using AvansDevOps.SprintFinish.Pipeline;
using AvansDevOps.SprintFinish.Pipeline.Commands;
using AvansDevOps.SprintFinish.Pipeline.Commands.Analyse;
using AvansDevOps.SprintFinish.Pipeline.Commands.Build;
using AvansDevOps.SprintFinish.Pipeline.Commands.Test;
using AvansDevOps.SprintFinish.Pipeline.PipelineFactory;

namespace AvansDevOps.Tests.Tests;

public class TestNotifier : INotification
{
    public string LastMessage { get; private set; }

    public void SendNotification(string message)
    {
        LastMessage = message;
    }
}


public class TestFactory : IPipelineToolFactory
{
    public GetSources CreateSourceAction()
    {
        return null;
    }

    public PackageInstaller CreatePackageAction()
    {
        return null;
    }

    public IBuildStrategy CreateBuildAction()
    {
        return null;
    }

    public ITestStrategy CreateTestAction()
    {
        return null;
    }

    public IAnalyseTemplate CreateAnalyseAction()
    {
        return null;
    }

    public Deploy CreateDeploymentAction()
    {
        return null;
    }

    public Utility CreateUtilityAction()
    {
        return null;
    }
}

[TestFixture]
public class ReleaseSprintTest

{
    [SetUp]
    public void SetUp()
    {
        _notifier = new TestNotifier();
        _strategy = new ReleaseSprintStrategy(_notifier, new TestFactory());
    }

    private ReleaseSprintStrategy _strategy;
    private TestNotifier _notifier;

    [Test]
    public void IsFinished_Default_IsFalse()
    {
        Assert.That(_strategy.IsFinished(), Is.False);
    }

    [Test]
    public void StartPipeline_WithSuccessfulPipeline_SetsIsReleasedTrue()
    {
        var pipeline = new Pipeline();
        pipeline.AddCommand(new TestCommand()); // success
        var handler = new PipelineHandler(pipeline);

        var result = _strategy.StartPipeline(handler);

        Assert.That(result, Is.True);
        Assert.That(_strategy.IsFinished(), Is.True);
    }

    [Test]
    public void StartPipeline_WithFailingPipeline_ThrowsExceptionAndIsReleasedFalse()
    {
        var pipeline = new Pipeline();
        pipeline.AddCommand(new TestCommand(true)); // will fail
        var handler = new PipelineHandler(pipeline);

        Assert.Throws<Exception>(() => _strategy.StartPipeline(handler));
        Assert.That(_strategy.IsFinished(), Is.False);
    }

    [Test]
    public void BuildPipeline_ReturnsPipeline()
    {
        var pipeline = _strategy.BuildPipeline();

        Assert.That(pipeline, Is.TypeOf<Pipeline>());
    }

    [Test]
    public void FinishSprint_DoesNothing()
    {
        // Just ensure calling it does not throw
        Assert.DoesNotThrow(() => _strategy.FinishSprint());
    }
}