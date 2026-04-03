using AvansDevOps.Notification;
using AvansDevOps.ProjectManagement;

namespace AvansDevOps.State;

public class DoneState : State
{
    public DoneState(Component component) : base(component) { }

    public override string GetNotificationMessage() => 
        $"The item \"{_component.GetName()}\" is finished!";

    public override void StartOver()
    {
        //start over all the way
        _component.ChangeState(new DoingState(_component));
    }
    
    
}