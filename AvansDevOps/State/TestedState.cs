using AvansDevOps.ProjectManagement;

namespace AvansDevOps.State;

public class TestedState : State
{
    public TestedState(Component component) : base(component) { }
    public override void SendNotification(String info)
    {
        
    }

    public override void Validated()
    {
        _component.ChangeState(new DoneState(_component));  
        //validated by scrummaster
    }

    public override void Invalidated()
    {
        _component.ChangeState(new DoingState(_component));
        //invalidated by scrummaster, go back to doing
    }
}