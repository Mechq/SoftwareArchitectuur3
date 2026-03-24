namespace AvansDevOps.State;

public class TestingState : IState
{
    public void SendNotification(String info)
    {
        //send notification to developer in case it fails
    }

    public void CompletedTests()
    {
        //tests passed
    }

    public void FailedTests()
    {
     //tests failed   
    }

    
}