namespace AvansDevOps.Notification;

public class SlackAdapter : Adapter
{
    public SlackAdapter(IService service) : base(service) { }

    public override void SendNotification(string info)
    {
        Adaptee.SendNotification(info);
    }
}