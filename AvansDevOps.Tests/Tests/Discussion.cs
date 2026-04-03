namespace DefaultNamespace;
using AvansDevOps.Notification;
using AvansDevOps.State;
using AvansDevOps.Thread;
using NUnit.Framework;

[TestFixture]

public class Discussion
{

    private INotification _notifier;
    private Discussion _discussion;

    [SetUp]
    public void SetUp()
    {
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
        _discussion.update(new DoneState()); // closes discussion

        _discussion.AddMessage("Should not be added");

        Assert.That(0, Is.EqualTo(_discussion.GetMessages().Count));
    }

    [Test]
    public void Update_FromOpenToDone_DisablesEditing()
    {
        _discussion.update(new DoneState());

        _discussion.AddMessage("Blocked message");

        Assert.That(0, Is.EqualTo(_discussion.GetMessages().Count));
    }

    [Test]
    public void Update_FromDoneToOpen_AllowsEditingAgain()
    {
        _discussion.update(new DoneState());
        _discussion.update(new ToDoState()); // or any non-DoneState

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
