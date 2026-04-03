using AvansDevOps.Notification;
using AvansDevOps.ProjectManagement;

namespace AvansDevOps.State;

public class DoingState : State
{
    public DoingState(Component component) : base(component) { }

    public override void TaskComplete()
    {
        //developer considers his task done
        Component.ChangeState(new ReadyForTestingState(Component));
    }
    
    public override string GetNotificationMessage() => 
        $"The item \"{Component.GetName()}\" has been picked up by a developer.";
}