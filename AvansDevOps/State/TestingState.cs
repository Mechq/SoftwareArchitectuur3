using AvansDevOps.ProjectManagement;

namespace AvansDevOps.State;

public class TestingState : State
{
    public TestingState(Component component) : base(component) { }

    public override void SendNotification(String info)
    {
        //send notification to developer in case it fails
    }

    public override void CompletedTests()
    {
        //tests passed
        _component.ChangeState(new TestedState(_component));
    }

    public override void FailedTests()
    {
     //tests failed, go back to doing   
     _component.ChangeState(new DoingState(_component));
    }

    
}