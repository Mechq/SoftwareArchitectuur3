using AvansDevOps.ProjectManagement;

namespace AvansDevOps.State;

public class DoneState : State
{
    public DoneState(Component component) : base(component) { }
    public override void SendNotification(String info)
    {
        //send noti to developer
    }

    public override void StartOver()
    {
        //start over all the way
        _component.ChangeState(new DoingState(_component));
    }
    
    
}