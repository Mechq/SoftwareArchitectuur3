using AvansDevOps.Notification;
using AvansDevOps.ProjectManagement;

namespace AvansDevOps.State;

public class TestedState : State
{
    public TestedState(Component component ) : base(component) { }

    public override string GetNotificationMessage() => 
        $"@ScrumMaster the item \"{_component.GetName()}\" has tested and is ready for review.";

    public override void Validated()
    {
        _component.ChangeState(new DoneState(_component));  
        //validated by scrum master
    }

    public override void Invalidated()
    {
        _component.ChangeState(new DoingState(_component));
        //invalidated by scrum master, go back to doing
    }
}