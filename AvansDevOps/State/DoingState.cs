using AvansDevOps.Notification;
using AvansDevOps.ProjectManagement;

namespace AvansDevOps.State;

public class DoingState : State
{
    public DoingState(Component component) : base(component) { }

    public override void TaskComplete()
    {
        //developer considers his task done
        _component.ChangeState(new ReadyForTestingState(_component));
    }
    
    public override string GetNotificationMessage() => 
        $"The item \"{_component.GetName()}\" has been picked up by a developer.";
}