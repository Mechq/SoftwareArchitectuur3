using AvansDevOps.Notification;
using AvansDevOps.ProjectManagement;
using AvansDevOps.ProjectManagement.Composite;
using AvansDevOps.SprintFinish;
using AvansDevOps.SprintFinish.Pipeline;
using AvansDevOps.User;
using AvansDevOps.VersionControl;
using AvansDevOps.VersionControl.Adapter;
using AvansDevOps.VersionControl.Service;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace AvansDevOps.Tests;

[TestFixture]
public class Project
{
    private INotification _slackNotifier;
    private IPipelineToolFactory _factory;
    private IVersionControl _gitHubCreator;
    private ProductOwner _productOwner;
    private ProjectComposite _project;

    [SetUp]
    public void SetUp()
    {
        _slackNotifier = new SlackAdapter(new SlackService());
        _factory = new DefaultPipelineFactory();
        _gitHubCreator = new GitHubAdapter(new GitHubService());
        _productOwner = new ProductOwner("Stef Rensma", "stef@gmail.com", "123", 31622434);
        _project = new ProjectComposite("Portfolio", _productOwner, _gitHubCreator);
    }
    

    [Test]
    public void Add_Sprint_Increases_SprintCount()
    {
        var sprint = CreateSprint("Sprint 1");
        _project.Add(sprint);
        Assert.That(1, Is.EqualTo(_project.GetSprints().Count));
    }

    [Test]
    public void Add_MultipleSprints_AllPresent()
    {
        _project.Add(CreateSprint("Sprint 1"));
        _project.Add(CreateSprint("Sprint 2"));
        Assert.That(2, Is.EqualTo(_project.GetSprints().Count));
    }

    [Test]
    public void Add_NonSprintComponent_ThrowsArgumentException()
    {
        var backlog = new BacklogComposite();
        Assert.Throws<ArgumentException>(() => _project.Add(backlog));
    }
    

    [Test]
    public void Remove_ExistingSprint_DecreasesCount()
    {
        var sprint = CreateSprint("Sprint 1");
        _project.Add(sprint);
        _project.Remove(sprint);
        Assert.That(0, Is.EqualTo(_project.GetSprints().Count));
    }

    [Test]
    public void Remove_NonSprintComponent_ThrowsArgumentException()
    {
        var backlog = new BacklogComposite();
        Assert.Throws<ArgumentException>(() => _project.Remove(backlog));
    }

    [Test]
    public void Remove_SprintNotInProject_DoesNotThrow()
    {
        var sprint = CreateSprint("Ghost Sprint");
        Assert.DoesNotThrow(() => _project.Remove(sprint));
        Assert.That(0, Is.EqualTo(_project.GetSprints().Count));
    }


    [Test]
    public void GetName_ReturnsCorrectName()
    {
        Assert.That("Portfolio", Is.EqualTo(_project.GetName()));

    }


    [Test]
    public void GetSprints_InitiallyEmpty()
    {
        Assert.That(_project.GetSprints(), Is.Empty);
    }

    [Test]
    public void GetSprints_ReturnsAddedSprints()
    {
        var sprint = CreateSprint("Sprint 1");
        _project.Add(sprint);
        CollectionAssert.Contains(_project.GetSprints(), sprint);
    }


    [Test]
    public void AddUser_UserAppearsInUserList()
    {
        var developer = new Developer("Jan Janssen", "jan@gmail.com", "pw", 123);
        _project.AddUser(developer);
        Assert.DoesNotThrow(() => _project.RemoveUser(developer));
    }

    [Test]
    public void AddUser_MultipleUsers_AllAdded()
    {
        var dev1 = new Developer("Alice", "alice@gmail.com", "pw", 1);
        var dev2 = new Developer("Bob", "bob@gmail.com", "pw", 2);
        Assert.DoesNotThrow(() =>
        {
            _project.AddUser(dev1);
            _project.AddUser(dev2);
        });
    }
    
    [Test]
    public void RemoveUser_ExistingUser_DoesNotThrow()
    {
        var developer = new Developer("Jan Janssen", "jan@gmail.com", "pw", 123);
        _project.AddUser(developer);
        Assert.DoesNotThrow(() => _project.RemoveUser(developer));
    }

    [Test]
    public void RemoveUser_UserNotInProject_DoesNotThrow()
    {
        var developer = new Developer("Ghost User", "ghost@gmail.com", "pw", 999);
        Assert.DoesNotThrow(() => _project.RemoveUser(developer));
    }

    

    private SprintComposite CreateSprint(string name)
    {
        var start = DateTime.Now;
        return new SprintComposite(name, start, start.AddDays(7),
            new FeedbackSprintStrategy(_slackNotifier, _factory));
    }
}