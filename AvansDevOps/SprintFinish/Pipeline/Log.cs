namespace AvansDevOps.SprintFinish.Pipeline;

public class Log
{
    public string message { get; set; }
    public DateTime date { get; set; }
    
    public string tag { get; set; }
    public Log(string message, string tag, DateTime date)
    {
        this.message = message;
        this.date = date;
        this.tag = tag;
    }

    public void Print()
    {
        Console.WriteLine($"[{tag}] \"{message}\" | {date}");
    }
    
}