using AvansDevOps.Notification;
using AvansDevOps.ProjectManagement;

namespace AvansDevOps.State;

public class ReadyForTestingState: State
{
    public ReadyForTestingState(Component component) : base(component) { }

    public override string GetNotificationMessage() => 
        $"@Testers the item \"{_component.GetName()}\" is ready for testing.";

    public override void StartedTesting()
    {
        //tester picks up this component
        _component.ChangeState(new TestingState(_component));
    }
}