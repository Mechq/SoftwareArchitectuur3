namespace AvansDevOps.Notification;

public class EmailService : IService
{
    public void SendNotification(String info)
    {
        Console.WriteLine("Sending email...");
        Console.WriteLine(info);
        Console.WriteLine("Sent email");
    }
}