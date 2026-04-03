using AvansDevOps.Notification;
using AvansDevOps.ProjectManagement;

namespace AvansDevOps.State;

public class DoneState : State
{
    public DoneState(Component component) : base(component) { }

    public override string GetNotificationMessage() => 
        $"The item \"{Component.GetName()}\" is finished!";

    public override void StartOver()
    {
        //start over all the way
        Component.ChangeState(new DoingState(Component));
    }
    
    
}