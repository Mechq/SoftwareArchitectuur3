namespace AvansDevOps.Notification;

public class EmailAdapter : Adapter
{

    public EmailAdapter(IService service) : base(service) { }

    public override void SendNotification(string info)
    {
        Adaptee.SendNotification(info);
    }
}