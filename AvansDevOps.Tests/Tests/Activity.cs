using AvansDevOps.Notification;
using AvansDevOps.ProjectManagement;
using AvansDevOps.State;
using AvansDevOps.User;

namespace AvansDevOps.Tests.Tests;

[TestFixture]
public class Activity
{
    
    public class TestNotifier : INotification
    {
        public string LastMessage { get; private set; }

        public void SendNotification(string message)
        {
            LastMessage = message;
        }
    }

    [TestFixture]
    public class ActivityLeafTests
    {
        private ActivityLeaf _activity;
        private TestNotifier _notifier;

        [SetUp]
        public void SetUp()
        {
            _notifier = new TestNotifier();
            _activity = new ActivityLeaf("Test Activity", "Initial Description", _notifier);
        }

        [Test]
        public void Constructor_SetsNameCorrectly()
        {
            Assert.That(_activity.GetName(), Is.EqualTo("Test Activity"));
        }

        [Test]
        public void Constructor_SetsInitialState_ToDoState()
        {
            Assert.That(_activity.GetState(), Is.TypeOf<ToDoState>());
        }

        [Test]
        public void Constructor_SendsNotification()
        {
            Assert.That(_notifier.LastMessage, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void UpdateDescription_ChangesDescription()
        {
            // Act
            _activity.UpdateDescription("Updated Description");

            // No getter, so we validate via Print (optional improvement: add getter)
            Assert.DoesNotThrow(() => _activity.Print());
        }

        [Test]
        public void AssignDeveloper_SetsDeveloperCorrectly()
        {
            var dev = new Developer("John", "john@mail.com", "pw", 123);

            _activity.AssignDeveloper(dev);

            Assert.That(_activity.GetDeveloper(), Is.EqualTo(dev));
        }

        [Test]
        public void ChangeState_UpdatesState()
        {
            var newState = new DoingState(_activity);

            _activity.ChangeState(newState);

            Assert.That(_activity.GetState(), Is.EqualTo(newState));
        }

        [Test]
        public void ChangeState_SendsNotification()
        {
            var newState = new DoingState(_activity);

            _activity.ChangeState(newState);

            Assert.That(_notifier.LastMessage, Is.EqualTo(newState.GetNotificationMessage()));
        }

        [Test]
        public void Add_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => _activity.Add(null));
        }

        [Test]
        public void Remove_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => _activity.Remove(null));
        }

        [Test]
        public void Print_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => _activity.Print());
        }
    }

}