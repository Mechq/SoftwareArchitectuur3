namespace AvansDevOps.Notification;

public class SlackService : IService
{
    public void SendNotification(String info)
    {
        Console.WriteLine("Sending slack message...");
        Console.WriteLine(info);
        Console.WriteLine("Sent slack message.");
    }
}