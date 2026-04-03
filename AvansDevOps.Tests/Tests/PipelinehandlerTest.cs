using AvansDevOps.SprintFinish.Pipeline;
using AvansDevOps.SprintFinish.Pipeline.Commands;
using AvansDevOps.SprintFinish.Pipeline.Commands.Build;
using AvansDevOps.SprintFinish.Pipeline.PipelineFactory;

namespace AvansDevOps.Tests.Tests;

public class TestCommand : ICommand
{
    private readonly bool _shouldFail;

    public TestCommand(bool shouldFail = false)
    {
        _shouldFail = shouldFail;
    }

    public void Execute()
    {
        if (_shouldFail)
            throw new Exception("Failure");
    }
}

[TestFixture]
public class PipelinehandlerTest
{
    [SetUp]
    public void SetUp()
    {
        _pipeline = new Pipeline();
        _handler = new PipelineHandler(_pipeline);
    }

    private Pipeline _pipeline;
    private PipelineHandler _handler;

    [Test]
    public void StartPipeline_WithSuccessfulCommands_ReturnsTrue()
    {
        _pipeline.AddCommand(new TestCommand());
        _pipeline.AddCommand(new TestCommand());

        var result = _handler.StartPipeline();

        Assert.That(result, Is.True);
    }

    [Test]
    public void StartPipeline_WhenCommandFails_ThrowsException()
    {
        _pipeline.AddCommand(new TestCommand(true));

        Assert.Throws<Exception>(() => _handler.StartPipeline());
    }

    [Test]
    public void RedoPipeline_WithSuccessfulCommands_ReturnsTrue()
    {
        _pipeline.AddCommand(new TestCommand());

        var result = _handler.RedoPipeline();

        Assert.That(result, Is.True);
    }

    [Test]
    public void RedoPipeline_ExecutesPipelineTwice()
    {
        var counter = 0;

        var command = new CountingCommand(() => counter++);

        _pipeline.AddCommand(command);

        _handler.RedoPipeline();

        // Should run twice
        Assert.That(counter, Is.EqualTo(2));
    }

    [Test]
    public void RedoPipeline_WhenCommandFails_ThrowsException()
    {
        _pipeline.AddCommand(new TestCommand(true));

        Assert.Throws<Exception>(() => _handler.RedoPipeline());
    }
    
    [Test]
    public void Pipeline_Failure_SendsNotification()
        {
            var pipeline = new Pipeline();
            pipeline.AddCommand(new TestCommand());

            Assert.Throws<Exception>(() => pipeline.Execute());
            Assert.That(pipeline.GetLogs()[0].GetTag(), Is.EqualTo("FAIL"));
        }


    [Test]
    public void Pipeline_CanBeBuilt_WithTwoDifferentBuildTypes()
    {
        
        var dotnetFactory = new DefaultPipelineFactory();
        var dotnetPipeline = new PipelineBuilder(dotnetFactory)
            .AddBuildStep()
            .GetResult();

        
        var mavenFactory = new MavenPipelineFactory();
        var mavenPipeline = new PipelineBuilder(mavenFactory)
            .AddBuildStep()
            .GetResult();

        Assert.DoesNotThrow(() => dotnetPipeline.Execute());
        Assert.DoesNotThrow(() => mavenPipeline.Execute());

        Assert.That(dotnetPipeline.GetLogs()[0].GetTag(), Is.EqualTo("SUCCESS"));
        Assert.That(mavenPipeline.GetLogs()[0].GetTag(), Is.EqualTo("SUCCESS"));
    }
}

// Helper command to count executions
public class CountingCommand : ICommand
{
    private readonly Action _onExecute;

    public CountingCommand(Action onExecute)
    {
        _onExecute = onExecute;
    }

    public void Execute()
    {
        _onExecute();
    }
}