using AvansDevOps.ProjectManagement;

namespace AvansDevOps.State;

public class ReadyForTestingState: State
{
    public ReadyForTestingState(Component component) : base(component) { }
    public override void SendNotification(String info)
    {
        
    }

    public override void StartedTesting()
    {
        //tester picks up this component
        _component.ChangeState(new TestingState(_component));
    }
}