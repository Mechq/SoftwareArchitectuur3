namespace AvansDevOps.Notification;

public abstract class Adapter : INotification
{
    protected readonly IService Adaptee;

    protected Adapter(IService service)
    {
        Adaptee = service;
    }
    public abstract void SendNotification(String info);
}