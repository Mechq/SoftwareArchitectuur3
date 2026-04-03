using AvansDevOps.State;
using AvansDevOps.Thread;
using AvansDevOps.User;
using AvansDevOps.VersionControl;

namespace AvansDevOps.ProjectManagement;

public class BacklogItemComposite : Component
{
    private string _name;
    private List<ActivityLeaf> _activities = [];
    private Developer _developer;
    private String _description;
    private State.State _state;
    private List<IBacklogObserver> _observers = [];
    private IVersionControl _versionControl;
    public BacklogItemComposite(string name, string description, IVersionControl versionControl)
    {
        _name = name;
        _description = description;
        _state = new ToDoState(this);
        _versionControl  = versionControl;
        versionControl.SendCommand("git branch " + _name);

    }

    public void Remove(Component component)
    {
        if (component is not ActivityLeaf)
        {
            throw new ArgumentException("Component must be of type ActivityLeaf");
        }
        _activities.Remove((ActivityLeaf)component);
    }

    public void Add(Component component)
    {
        if (component is not ActivityLeaf)
        {
            throw new ArgumentException("Component must be of type ActivityLeaf");
        }
        _activities.Add((ActivityLeaf)component);
    }

    public void Print(){Console.WriteLine(" This backlog item is called " + _name + " and contains the activities: "); PrintActivity();}
    
    
    public string GetName()
    {
        return  _name;
    }

    private void PrintActivity()
    {
        foreach (ActivityLeaf activity in _activities)
        {
            Console.WriteLine(activity.GetName());
        }
    }

    public List<ActivityLeaf> GetActivities()
    {
        return _activities;
    }

    public void AssignDeveloper(Developer developer)
    {
        _developer = developer;
    }
    
    public void ChangeState(State.State newState)
    {
        Console.WriteLine($"Context: Transition to {newState.GetType().Name}.");
        _state = newState;
        NotifyObservers();
    }

    public State.State GetState()
    {
        return _state;
    }
    
    public void PrintObservers()
    {
        foreach (IBacklogObserver observer in _observers)
        {
            Console.WriteLine(_observers.IndexOf(observer) + ". "  +observer.GetName());
        }
    }
    

    public void AddObserver(IBacklogObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IBacklogObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (IBacklogObserver observer in _observers)
        {
            observer.update(_state);
        }
    }
    
}