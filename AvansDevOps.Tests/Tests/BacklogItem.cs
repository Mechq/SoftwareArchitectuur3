using AvansDevOps.Notification;
using AvansDevOps.ProjectManagement;
using AvansDevOps.ProjectManagement.Composite;
using AvansDevOps.State;
using AvansDevOps.Thread;
using AvansDevOps.User;
using AvansDevOps.VersionControl;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace AvansDevOps.Tests.Tests;

[TestFixture]
public class BacklogItem
{
    private INotification _notifier;
    private IVersionControl _versionControl;
    private BacklogItemComposite _backlogItem;

    [SetUp]
    public void SetUp()
    {
        _notifier = new SlackAdapter(new SlackService());
        _versionControl = new GitHubAdapter(new GitHubService());
        _backlogItem = new BacklogItemComposite("Login Feature", "As a user I want to log in", _versionControl, _notifier);
    }


    [Test]
    public void Add_ActivityLeaf_IncreasesActivityCount()
    {
        var activity = new ActivityLeaf("Write tests", " is an important aspect", _notifier);
        _backlogItem.Add(activity);
        Assert.That(1, Is.EqualTo(_backlogItem.GetActivities().Count));

    }

    [Test]
    public void Add_MultipleActivities_AllPresent()
    {
        _backlogItem.Add(new ActivityLeaf("Write tests", " is an important aspect", _notifier));
        _backlogItem.Add(new ActivityLeaf("Write implementation", " is an important aspect", _notifier));
        Assert.That(2, Is.EqualTo(_backlogItem.GetActivities().Count));
    }

    [Test]
    public void Add_NonActivityLeaf_ThrowsArgumentException()
    {
        var backlog = new BacklogComposite();
        Assert.Throws<ArgumentException>(() => _backlogItem.Add(backlog));
    }


    [Test]
    public void Remove_ExistingActivity_DecreasesCount()
    {
        var activity = new ActivityLeaf("Write tests", " is an important aspect", _notifier);
        _backlogItem.Add(activity);
        _backlogItem.Remove(activity);
        Assert.That(0, Is.EqualTo(_backlogItem.GetActivities().Count));
    }

    [Test]
    public void Remove_NonActivityLeaf_ThrowsArgumentException()
    {
        var backlog = new BacklogComposite();
        Assert.Throws<ArgumentException>(() => _backlogItem.Remove(backlog));
    }

    [Test]
    public void Remove_ActivityNotPresent_DoesNotThrow()
    {
        var activity = new ActivityLeaf("Ghost activity", " spooky", _notifier);
        Assert.DoesNotThrow(() => _backlogItem.Remove(activity));
    }


    [Test]
    public void GetName_ReturnsCorrectName()
    {
        Assert.That("Login Feature", Is.EqualTo(_backlogItem.GetName()));

    }


    [Test]
    public void GetActivities_InitiallyEmpty()
    {
        Assert.That(_backlogItem.GetActivities(), Is.Empty);
    }

    [Test]
    public void GetActivities_ReturnsAddedActivities()
    {
        var activity = new ActivityLeaf("Write tests", " is an important aspect", _notifier);
        _backlogItem.Add(activity);
        CollectionAssert.Contains(_backlogItem.GetActivities(), activity);
    }


    [Test]
    public void AssignDeveloper_DoesNotThrow()
    {
        var developer = new Developer("Alice", "alice@gmail.com", "pw", 1);
        Assert.DoesNotThrow(() => _backlogItem.AssignDeveloper(developer));
    }

    [Test]
    public void AssignDeveloper_CanReassign_DoesNotThrow()
    {
        var dev1 = new Developer("Alice", "alice@gmail.com", "pw", 1);
        var dev2 = new Developer("Bob", "bob@gmail.com", "pw", 2);
        _backlogItem.AssignDeveloper(dev1);
        Assert.DoesNotThrow(() => _backlogItem.AssignDeveloper(dev2));
    }


    [Test]
    public void GetState_InitialState_IsToDoState()
    {
        Assert.That(_backlogItem.GetState(), Is.InstanceOf<ToDoState>());
    }

    [Test]
    public void ChangeState_UpdatesState()
    {
        _backlogItem.ChangeState(new DoingState(_backlogItem));
        Assert.That(_backlogItem.GetState(), Is.InstanceOf<DoingState>());

    }

    [Test]
    public void ChangeState_NotifiesObservers()
    {
        var observer = new MockBacklogObserver();
        _backlogItem.AddObserver(observer);
        _backlogItem.ChangeState(new DoingState(_backlogItem));
        Assert.That(observer.WasNotified, Is.True);
    }


    [Test]
    public void AddObserver_ObserverGetsNotifiedOnStateChange()
    {
        var observer = new MockBacklogObserver();
        _backlogItem.AddObserver(observer);
        _backlogItem.ChangeState(new DoingState(_backlogItem));
        Assert.That(observer.WasNotified, Is.True);
    }

    [Test]
    public void RemoveObserver_ObserverNoLongerNotified()
    {
        var observer = new MockBacklogObserver();
        _backlogItem.AddObserver(observer);
        _backlogItem.RemoveObserver(observer);
        _backlogItem.ChangeState(new DoingState(_backlogItem));
        Assert.That(observer.WasNotified, Is.False);
    }

    [Test]
    public void RemoveObserver_NotPresent_DoesNotThrow()
    {
        var observer = new MockBacklogObserver();
        Assert.DoesNotThrow(() => _backlogItem.RemoveObserver(observer));
    }

    [Test]
    public void NotifyObservers_MultipleObservers_AllNotified()
    {
        var obs1 = new MockBacklogObserver();
        var obs2 = new MockBacklogObserver();
        _backlogItem.AddObserver(obs1);
        _backlogItem.AddObserver(obs2);
        _backlogItem.NotifyObservers();
        Assert.That(obs1.WasNotified, Is.True);
        Assert.That(obs2.WasNotified, Is.True);
    }

    [Test]
    public void NotifyObservers_NoObservers_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => _backlogItem.NotifyObservers());
    }
    
    [Test]
    public void ReadyForTesting_CannotTransitionBack_ToDoing()
    {
        var item = new BacklogItemComposite("Login", "description", _versionControl, _notifier);
        item.ChangeState(new DoingState(item));
        item.ChangeState(new ReadyForTestingState(item));

        
        item.GetState().TaskComplete();

        Assert.That(item.GetState(), Is.Not.InstanceOf<DoingState>());
    }
}


public class MockBacklogObserver : IBacklogObserver
{
    public bool WasNotified { get; private set; }
    public State.State LastState { get; private set; }
    private List<string> _messages = [];

    public void Update(State.State state)
    {
        WasNotified = true;
        LastState = state;
    }

    public string GetName() => "MockObserver";

    public List<string> GetMessages() => _messages;

    public void AddMessage(string message) => _messages.Add(message);
}
