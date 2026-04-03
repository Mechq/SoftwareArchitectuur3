using AvansDevOps.Notification;
using AvansDevOps.ProjectManagement;
using AvansDevOps.ProjectManagement.Composite;
using AvansDevOps.SprintFinish;
using AvansDevOps.SprintFinish.Pipeline;
using AvansDevOps.User;

namespace AvansDevOps.Tests.Tests;

using AvansDevOps.ProjectManagement;
using AvansDevOps.SprintFinish;
using AvansDevOps.User;
using NUnit.Framework;


[TestFixture]
public class Sprint
{
    private SprintComposite _sprint;
    private ISprintStrategy _strategy;

    [SetUp]
    public void SetUp()
    {
        INotification emailNotifier = new EmailAdapter(new EmailService());
        IPipelineToolFactory factory = new DefaultPipelineFactory();
        _strategy = new FeedbackSprintStrategy(emailNotifier, factory); // simplest strategy
        _sprint = new SprintComposite(
            "Sprint 1",
            DateTime.Now.AddDays(-1),
            DateTime.Now.AddDays(5),
            _strategy
        );
    }

    [Test]
    public void GetName_ReturnsCorrectName()
    {
        Assert.That("Sprint 1", Is.EqualTo(_sprint.GetName()));
    }

    [Test]
    public void CanEdit_BeforeEndDate_ReturnsTrue()
    {
        Assert.That(_sprint.CanEdit(), Is.True);
    }

    [Test]
    public void CanEdit_AfterEndDate_ReturnsFalse()
    {
        var sprint = new SprintComposite(
            "Old Sprint",
            DateTime.Now.AddDays(-10),
            DateTime.Now.AddDays(-1),
            _strategy
        );

        Assert.That(sprint.CanEdit(), Is.False);
    }

    [Test]
    public void AssignScrumMaster_WhenEditable_AssignsScrumMaster()
    {
        var scrumMaster = new ScrumMaster("John", "john@gmail.com", "pw", 1234);

        _sprint.AssignScrumMaster(scrumMaster);
        
        Assert.DoesNotThrow(() => _sprint.Print());
    }

    [Test]
    public void AssignScrumMaster_WhenNotEditable_DoesNotThrow()
    {
        var sprint = new SprintComposite(
            "Closed Sprint",
            DateTime.Now.AddDays(-10),
            DateTime.Now.AddDays(-1),
            _strategy
        );

        var scrumMaster = new ScrumMaster("John", "john@gmail.com", "pw", 1234);

        Assert.DoesNotThrow(() => sprint.AssignScrumMaster(scrumMaster));
    }

    [Test]
    public void IsSuccessfulSprint_Default_IsFalse()
    {
        Assert.That(_sprint.IsSuccessfulSprint(), Is.False);
    }

    [Test]
    public void Add_DoesNotThrow()
    {
        var component = new BacklogComposite();

        Assert.DoesNotThrow(() => _sprint.Add(component));
    }

    [Test]
    public void Remove_DoesNotThrow()
    {
        var component = new BacklogComposite();

        Assert.DoesNotThrow(() => _sprint.Remove(component));
    }

    [Test]
    public void RunPipeline_WhenEditable_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => _sprint.RunPipeline());
    }

    [Test]
    public void RunPipeline_WhenNotEditable_DoesNotThrow()
    {
        var sprint = new SprintComposite(
            "Closed Sprint",
            DateTime.Now.AddDays(-10),
            DateTime.Now.AddDays(-1),
            _strategy
        );

        Assert.DoesNotThrow(() => sprint.RunPipeline());
    }
}