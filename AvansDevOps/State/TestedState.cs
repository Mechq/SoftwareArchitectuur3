using AvansDevOps.Notification;
using AvansDevOps.ProjectManagement;

namespace AvansDevOps.State;

public class TestedState : State
{
    public TestedState(Component component ) : base(component) { }

    public override string GetNotificationMessage() => 
        $"@ScrumMaster the item \"{Component.GetName()}\" has tested and is ready for review.";

    public override void Validated()
    {
        Component.ChangeState(new DoneState(Component));  
        //validated by scrum master
    }

    public override void Invalidated()
    {
        Component.ChangeState(new DoingState(Component));
        //invalidated by scrum master, go back to doing
    }
}