using AvansDevOps.ProjectManagement;

namespace AvansDevOps.State;

public class ToDoState : State
{
    
    public ToDoState(Component component) : base(component) { }
    public override void SendNotification(String info)
    {
        
    }
    public override void StartedWorking()
    {
        //picked up by developer
        _component.ChangeState(new DoingState(_component));
    }

    
}