namespace AvansDevOps.State;

public class DoingState: IState
{
    public void SendNotification(String info)
    {
        //send notification to testers
    }
    public void TaskComplete()
    {
        //developer considers his task done
    }
}