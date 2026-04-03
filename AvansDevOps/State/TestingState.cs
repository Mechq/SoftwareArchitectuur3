using AvansDevOps.ProjectManagement;

namespace AvansDevOps.State;

public class TestingState : State
{
    public TestingState(Component component) : base(component) { }


   public override string GetNotificationMessage() => 
       $"The item \"{_component.GetName()}\" has been picked up by a tester.";

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