namespace AvansDevOps.State;

public class ReadyForTestingState: IState
{
    public void SendNotification(String info)
    {
        
    }

    public void StartedTesting()
    {
        //tester picks up this component
    }
}