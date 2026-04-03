using AvansDevOps.Notification;
using AvansDevOps.ProjectManagement;

namespace AvansDevOps.State;

public class ReadyForTestingState: State
{
    public ReadyForTestingState(Component component) : base(component) { }

    public override string GetNotificationMessage() => 
        $"@Testers the item \"{Component.GetName()}\" is ready for testing.";

    public override void StartedTesting()
    {
        //Tester picks up this component
        Component.ChangeState(new TestingState(Component));
    }
}