using AvansDevOps.ProjectManagement;
using AvansDevOps.VersionControl;
using AvansDevOps.Notification;
using AvansDevOps.ProjectManagement.Composite;
using AvansDevOps.State;
using AvansDevOps.Thread;
using NUnit.Framework;
namespace AvansDevOps.Tests.Tests;


[TestFixture]

public class DiscussionTest
{
    private IVersionControl _versionControl;
    private INotification _notifier;
    private Discussion _discussion;

    [SetUp]
    public void SetUp()
    {
        _versionControl = new GitHubAdapter(new GitHubService());
        _notifier = new SlackAdapter(new SlackService());
        _discussion = new Discussion("General", new List<string>(), _notifier);
    }

    [Test]
    public void AddMessage_WhenEditable_AddsMessage()
    {
        _discussion.AddMessage("Hello world");

        Assert.That(1, Is.EqualTo(_discussion.GetMessages().Count));
        Assert.That("Hello world", Is.EqualTo(_discussion.GetMessages()[0]));
    }

    [Test]
    public void AddMessage_MultipleMessages_AllPresent()
    {
        _discussion.AddMessage("First");
        _discussion.AddMessage("Second");

        Assert.That(2, Is.EqualTo(_discussion.GetMessages().Count));
    }

    [Test]
    public void AddMessage_WhenClosed_DoesNotAddMessage()
    {
        var component = new BacklogItemComposite("Test", "desc", _versionControl, _notifier);
        _discussion.Update(new DoneState(component));
        _discussion.AddMessage("Should not be added");

        Assert.That(0, Is.EqualTo(_discussion.GetMessages().Count));
    }

    [Test]
    public void Update_FromOpenToDone_DisablesEditing()
    {
        var component = new BacklogItemComposite("Test", "desc", _versionControl, _notifier);
        _discussion.Update(new DoneState(component));
        _discussion.AddMessage("Blocked message");

        Assert.That(0, Is.EqualTo(_discussion.GetMessages().Count));
    }

    [Test]
    public void Update_FromDoneToOpen_AllowsEditingAgain()
    {
        var component = new BacklogItemComposite("Test", "desc", _versionControl, _notifier);
        _discussion.Update(new DoneState(component));
        _discussion.Update(new ToDoState(component));

        _discussion.AddMessage("Now allowed");

        Assert.That(1, Is.EqualTo(_discussion.GetMessages().Count));
    }

    [Test]
    public void GetName_ReturnsCorrectName()
    {
        Assert.That("General", Is.EqualTo(_discussion.GetName()));
    }

    [Test]
    public void GetMessages_ReturnsSameListReference()
    {
        var messages = _discussion.GetMessages();

        messages.Add("Injected");

        Assert.That(1, Is.EqualTo(_discussion.GetMessages().Count));
    }
}
