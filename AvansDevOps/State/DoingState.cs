using AvansDevOps.ProjectManagement;

namespace AvansDevOps.State;

public class DoingState : State
{
    public DoingState(Component component) : base(component) { }
    
    public override void SendNotification(String info)
    {
        //send notification to testers
    }
    public override void TaskComplete()
    {
        //developer considers his task done
        _component.ChangeState(new ReadyForTestingState(_component));
    }
}