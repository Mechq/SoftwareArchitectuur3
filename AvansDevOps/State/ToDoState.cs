using AvansDevOps.Notification;
using AvansDevOps.ProjectManagement;

namespace AvansDevOps.State;

public class ToDoState : State
{
    
    public ToDoState(Component component) : base(component) { }

    public override string GetNotificationMessage() => 
        $"@Developers the item \"{_component.GetName()}\" has been added to the backlog.";
    public override void StartedWorking()
    {
        //picked up by developer
        _component.ChangeState(new DoingState(_component));
    }

    
}