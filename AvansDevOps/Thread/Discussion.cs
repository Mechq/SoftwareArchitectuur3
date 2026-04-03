using AvansDevOps.Notification;
using AvansDevOps.State;

namespace AvansDevOps.Thread;

public class Discussion : IBacklogObserver
{
    private String _name;
    private List<String> _messages = [];
    private bool _canEdit = true;
    private readonly INotification _notifier;

    public Discussion(String name, List<String> messages, INotification notifier)
    {
        _name = name;
        _messages = messages;
        _notifier = notifier;
    }

    public void AddMessage(String message)
    {
        if (_canEdit)
        {
            _messages.Add(message);
            _notifier.SendNotification(message); 
        }
        else
        {
            Console.WriteLine("This discussion is closed");
        }
        
        
    }

    public List<String> GetMessages()
    {
        return _messages;
    }

    public String GetName()
    {
        return _name;
    } 
    
    public void update(State.State state)
    {
        _canEdit = state.GetType() != typeof(State.DoneState); //done == no edit
    }
}