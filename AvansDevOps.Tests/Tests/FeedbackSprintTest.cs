using AvansDevOps.Notification;
using AvansDevOps.SprintFinish;
using AvansDevOps.SprintFinish.Pipeline;
using AvansDevOps.SprintFinish.Pipeline.Commands;
using AvansDevOps.SprintFinish.Pipeline.Commands.Analyse;
using AvansDevOps.SprintFinish.Pipeline.Commands.Build;
using AvansDevOps.SprintFinish.Pipeline.Commands.Test;
using AvansDevOps.SprintFinish.Pipeline.PipelineFactory;

namespace AvansDevOps.Tests.Tests;

[TestFixture]
public class FeedbackSprintTest

{
    // ---------------- Test Doubles ----------------

    public class TestNotifier : INotification
    {
        public string LastMessage { get; private set; }

        public void SendNotification(string message)
        {
            LastMessage = message;
        }
    }

    public class TestCommand : ICommand
    {
        private readonly bool _shouldThrow;
        private readonly Action _onExecute;

        public TestCommand(bool shouldThrow = false, Action onExecute = null)
        {
            _shouldThrow = shouldThrow;
            _onExecute = onExecute;
        }

        public void Execute()
        {
            _onExecute?.Invoke();

            if (_shouldThrow)
                throw new Exception("Command failed");
        }
    }

    public class TestFactory : IPipelineToolFactory
    {
        public GetSources CreateSourceAction() => null;
        public PackageInstaller CreatePackageAction() => null;
        public IBuildStrategy CreateBuildAction() => null;
        public ITestStrategy CreateTestAction() => null;
        public IAnalyseTemplate CreateAnalyseAction() => null;
        public Deploy CreateDeploymentAction() => null;
        public Utility CreateUtilityAction() => null;
    }

    // ---------------- FeedbackSprintStrategy Tests ----------------

    [TestFixture]
    public class FeedbackSprintStrategyTests
    {
        private FeedbackSprintStrategy _strategy;
        private TestNotifier _notifier;

        [SetUp]
        public void SetUp()
        {
            _notifier = new TestNotifier();
            _strategy = new FeedbackSprintStrategy(_notifier, new TestFactory());
        }

        [Test]
        public void IsFinished_Default_IsFalse()
        {
            Assert.That(_strategy.IsFinished(), Is.False);
        }

        [Test]
        public void UploadedSummary_SetsFinishedToTrue()
        {
            _strategy.UploadedSummary();

            Assert.That(_strategy.IsFinished(), Is.True);
        }

        [Test]
        public void FinishSprint_WithoutSummary_DoesNotSendNotification()
        {
            _strategy.FinishSprint();

            Assert.That(_notifier.LastMessage, Is.Null);
        }

        [Test]
        public void FinishSprint_WithSummary_SendsNotification()
        {
            _strategy.UploadedSummary();

            _strategy.FinishSprint();

            Assert.That(_notifier.LastMessage, Is.EqualTo("Sprint finished."));
        }

        [Test]
        public void StartPipeline_DelegatesToHandler()
        {
            var pipeline = new Pipeline();
            pipeline.AddCommand(new TestCommand()); // success

            var handler = new PipelineHandler(pipeline);

            var result = _strategy.StartPipeline(handler);

            Assert.That(result, Is.True);
        }

        [Test]
        public void StartPipeline_WhenPipelineFails_Throws()
        {
            var pipeline = new Pipeline();
            pipeline.AddCommand(new TestCommand(true)); // will fail

            var handler = new PipelineHandler(pipeline);

            Assert.Throws<Exception>(() => _strategy.StartPipeline(handler));
        }

        [Test]
        public void BuildPipeline_ReturnsPipeline()
        {
            var pipeline = _strategy.BuildPipeline();

            Assert.That(pipeline, Is.TypeOf<Pipeline>());
        }
    }

    // ---------------- Pipeline Tests ----------------

    [TestFixture]
    public class PipelineTests
    {
        private Pipeline _pipeline;

        [SetUp]
        public void SetUp()
        {
            _pipeline = new Pipeline();
        }

        [Test]
        public void Execute_WithSuccessfulCommands_ReturnsTrue()
        {
            _pipeline.AddCommand(new TestCommand());
            _pipeline.AddCommand(new TestCommand());

            var result = _pipeline.Execute();

            Assert.That(result, Is.True);
        }

    

        [Test]
        public void Execute_WhenCommandFails_ThrowsException()
        {
            _pipeline.AddCommand(new TestCommand(true));

            Assert.Throws<Exception>(() => _pipeline.Execute());
        }

       

        [Test]
        public void Execute_ExecutesAllCommandsInOrder()
        {
            int counter = 0;

            _pipeline.AddCommand(new TestCommand(onExecute: () => Assert.That(counter++, Is.EqualTo(0))));
            _pipeline.AddCommand(new TestCommand(onExecute: () => Assert.That(counter++, Is.EqualTo(1))));

            _pipeline.Execute();

            Assert.That(counter, Is.EqualTo(2));
        }
    }
}