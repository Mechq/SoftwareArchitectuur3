using AvansDevOps.SprintFinish.Pipeline;
using AvansDevOps.SprintFinish.Pipeline.Commands;

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