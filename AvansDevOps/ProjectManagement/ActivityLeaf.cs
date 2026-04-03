using AvansDevOps.State;
using AvansDevOps.User;
using AvansDevOps.Notification;

namespace AvansDevOps.ProjectManagement;

public class ActivityLeaf : Component
{
    private String _name;
    private String _description;
    private State.State _state;
    private Developer _developer;
    private INotification _notifier;
    
    public ActivityLeaf(string name, string description, INotification notifier)
    {
        _name = name;
        _description = description;
        _state = new ToDoState(this);
        _notifier = notifier;
        _notifier.SendNotification(_state.GetNotificationMessage());
    }

    public void Remove(Component component)
    {
        Console.WriteLine("Invalid method for this class");
    }

    public void UpdateDescription(String description)
    {
        //optionally add checks
        _description = description;
    }

    public void Add(Component component)
    {
        Console.WriteLine("Invalid method for this class");
    }

    public void Print()
    {
        Console.WriteLine($"{_name} - {_description}");
    }
    
    public string GetName()
    {
        return _name;
    }

    public void AssignDeveloper(Developer developer)
    {
        _developer  = developer;
    }

    public void ChangeState(State.State newState)
    {
        Console.WriteLine($"Context: Transition to {newState.GetType().Name}.");
        _state = newState;
        _notifier.SendNotification(_state.GetNotificationMessage());
    }

    public State.State GetState()
    {
        return _state;
    }
    
}