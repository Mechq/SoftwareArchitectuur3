using AvansDevOps.ProjectManagement;

namespace AvansDevOps.State;

public class TestingState : State
{
    public TestingState(Component component) : base(component) { }


   public override string GetNotificationMessage() => 
       $"The item \"{Component.GetName()}\" has been picked up by a tester.";

    public override void CompletedTests()
    {
        //tests passed
        Component.ChangeState(new TestedState(Component));
    }

    public override void FailedTests()
    {
     //tests failed, go back to doing   
     Component.ChangeState(new DoingState(Component));
    }

    
}