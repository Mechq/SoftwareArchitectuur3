using AvansDevOps.Notification;
using AvansDevOps.ProjectManagement;
using AvansDevOps.State;

namespace AvansDevOps.Tests.Tests;

[TestFixture]
public class StateTesting
{
    // Simple notifier stub
    public class TestNotifier : INotification
    {
        public string LastMessage { get; private set; }

        public void SendNotification(string message)
        {
            LastMessage = message;
        }
    }

    [TestFixture]
    public class StateTests
    {
        private ActivityLeaf _activity;
        private TestNotifier _notifier;

        [SetUp]
        public void SetUp()
        {
            _notifier = new TestNotifier();
            _activity = new ActivityLeaf("TestItem", "Desc", _notifier);
        }

        // ---------------- ToDoState ----------------

        [Test]
        public void ToDoState_GetNotificationMessage_IsCorrect()
        {
            var state = new ToDoState(_activity);

            var message = state.GetNotificationMessage();

            Assert.That(message, Does.Contain("has been added to the backlog"));
        }

        [Test]
        public void ToDoState_StartedWorking_TransitionsToDoing()
        {
            var state = new ToDoState(_activity);

            state.StartedWorking();

            Assert.That(_activity.GetState(), Is.TypeOf<DoingState>());
        }

        // ---------------- DoingState ----------------

        [Test]
        public void DoingState_GetNotificationMessage_IsCorrect()
        {
            var state = new DoingState(_activity);

            var message = state.GetNotificationMessage();

            Assert.That(message, Does.Contain("picked up by a developer"));
        }

        [Test]
        public void DoingState_TaskComplete_TransitionsToReadyForTesting()
        {
            var state = new DoingState(_activity);

            state.TaskComplete();

            Assert.That(_activity.GetState(), Is.TypeOf<ReadyForTestingState>());
        }

        // ---------------- ReadyForTestingState ----------------

        [Test]
        public void ReadyForTestingState_GetNotificationMessage_IsCorrect()
        {
            var state = new ReadyForTestingState(_activity);

            var message = state.GetNotificationMessage();

            Assert.That(message, Does.Contain("ready for testing"));
        }

        [Test]
        public void ReadyForTestingState_StartedTesting_TransitionsToTesting()
        {
            var state = new ReadyForTestingState(_activity);

            state.StartedTesting();

            Assert.That(_activity.GetState(), Is.TypeOf<TestingState>());
        }

        // ---------------- TestingState ----------------

        [Test]
        public void TestingState_GetNotificationMessage_IsCorrect()
        {
            var state = new TestingState(_activity);

            var message = state.GetNotificationMessage();

            Assert.That(message, Does.Contain("picked up by a tester"));
        }

        [Test]
        public void TestingState_CompletedTests_TransitionsToTested()
        {
            var state = new TestingState(_activity);

            state.CompletedTests();

            Assert.That(_activity.GetState(), Is.TypeOf<TestedState>());
        }

        [Test]
        public void TestingState_FailedTests_TransitionsToDoing()
        {
            var state = new TestingState(_activity);

            state.FailedTests();

            Assert.That(_activity.GetState(), Is.TypeOf<DoingState>());
        }

        // ---------------- TestedState ----------------

        [Test]
        public void TestedState_GetNotificationMessage_IsCorrect()
        {
            var state = new TestedState(_activity);

            var message = state.GetNotificationMessage();

            Assert.That(message, Does.Contain("ready for review"));
        }

        [Test]
        public void TestedState_Validated_TransitionsToDone()
        {
            var state = new TestedState(_activity);

            state.Validated();

            Assert.That(_activity.GetState(), Is.TypeOf<DoneState>());
        }

        [Test]
        public void TestedState_Invalidated_TransitionsToDoing()
        {
            var state = new TestedState(_activity);

            state.Invalidated();

            Assert.That(_activity.GetState(), Is.TypeOf<DoingState>());
        }

        // ---------------- DoneState ----------------

        [Test]
        public void DoneState_GetNotificationMessage_IsCorrect()
        {
            var state = new DoneState(_activity);

            var message = state.GetNotificationMessage();

            Assert.That(message, Does.Contain("is finished"));
        }

        [Test]
        public void DoneState_StartOver_TransitionsToDoing()
        {
            var state = new DoneState(_activity);

            state.StartOver();

            Assert.That(_activity.GetState(), Is.TypeOf<DoingState>());
        }
    }
}
