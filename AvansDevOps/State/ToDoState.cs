using AvansDevOps.Notification;
using AvansDevOps.ProjectManagement;

namespace AvansDevOps.State;

public class ToDoState : State
{
    
    public ToDoState(Component component) : base(component) { }

    public override string GetNotificationMessage() => 
        $"@Developers the item \"{Component.GetName()}\" has been added to the backlog.";
    public override void StartedWorking()
    {
        //picked up by developer
        Component.ChangeState(new DoingState(Component));
    }

    
}