using AvansDevOps.Notification;
using AvansDevOps.ProjectManagement;
using AvansDevOps.VersionControl;
using NUnit.Framework;

namespace AvansDevOps.Tests;

[TestFixture]
public class Backlog
{
 
    private INotification _notifier;
    private IVersionControl _versionControl;
    private BacklogComposite _backlog;

    [SetUp]
    public void SetUp()
    {
        _notifier = new SlackAdapter(new SlackService());
        _versionControl = new GitHubAdapter(new GitHubService());
        _backlog = new BacklogComposite();
    }

    [Test]
    public void Add_BacklogItem_IncreasesCount()
    {
        var item = CreateItem("Login Feature");
        _backlog.Add(item);
        Assert.That(1, Is.EqualTo(_backlog.GetBacklogItems().Count));
    }

    [Test]
    public void Add_MultipleItems_AllPresent()
    {
        _backlog.Add(CreateItem("Login Feature"));
        _backlog.Add(CreateItem("Register Feature"));
        Assert.That(2, Is.EqualTo(_backlog.GetBacklogItems().Count));

    }

    [Test]
    public void Add_NonBacklogItem_ThrowsInvalidCastException()
    {
        var activity = new ActivityLeaf("Some activity", " do something", _notifier);
        Assert.Throws<InvalidCastException>(() => _backlog.Add(activity));
    }

    [Test]
    public void Remove_ExistingItem_DecreasesCount()
    {
        var item = CreateItem("Login Feature");
        _backlog.Add(item);
        _backlog.Remove(item);
        Assert.That(0, Is.EqualTo(_backlog.GetBacklogItems().Count));
    }

    [Test]
    public void Remove_ItemNotPresent_DoesNotThrow()
    {
        var item = CreateItem("Ghost Item");
        Assert.DoesNotThrow(() => _backlog.Remove(item));
    }

    
    [Test]
    public void GetName_ReturnsBacklog()
    {
        Assert.That("Backlog", Is.EqualTo(_backlog.GetName()));

    }

    
    [Test]
    public void MoveBacklogItem_MovesItemToDestination()
    {
        var item = CreateItem("Login Feature");
        var destination = new BacklogComposite();
        _backlog.Add(item);

        _backlog.MoveBacklogItem(item, destination);

        Assert.That(0, Is.EqualTo(_backlog.GetBacklogItems().Count));
        
        Assert.That(1, Is.EqualTo(destination.GetBacklogItems().Count));
    }

    [Test]
    public void MoveBacklogItem_ItemNotInSource_DestinationUnchanged()
    {
        var item = CreateItem("Ghost Item");
        var destination = new BacklogComposite();

        _backlog.MoveBacklogItem(item, destination);

        Assert.That(0, Is.EqualTo(destination.GetBacklogItems().Count));
    }

    [Test]
    public void MoveBacklogItem_ItemNotInSource_SourceUnchanged()
    {
        var item = CreateItem("Ghost Item");
        var destination = new BacklogComposite();

        _backlog.MoveBacklogItem(item, destination);

        Assert.That(0, Is.EqualTo(_backlog.GetBacklogItems().Count));
    }

    
    private BacklogItemComposite CreateItem(string name)
    {
        return new BacklogItemComposite(name, "Some description", _versionControl, _notifier);
    }

}