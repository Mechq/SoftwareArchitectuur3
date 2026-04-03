namespace AvansDevOps.SprintFinish.Pipeline;

public class Log
{
    private readonly string _message;
    private readonly DateTime _date;
    private readonly string _tag;
    public Log(string message, string tag, DateTime date)
    {
        _message = message;
        _date = date;
        _tag = tag;
    }

    public void Print()
    {
        Console.WriteLine($"[{_tag}] \"{_message}\" | {_date}");
    }
    
}