using AvansDevOps.State;
using AvansDevOps.User;

namespace AvansDevOps.ProjectManagement;

public class BacklogItemComposite : Component
{
    private string _name;
    private List<ActivityLeaf> _activities = [];
    private Developer _developer;
    private String _description;
    private State.State _state;
    //private List<Observer> _observers;
    public BacklogItemComposite(string name, string description)
    {
        _name = name;
        _description = description;
        _state = new ToDoState(this);

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

    public void Print(){Console.WriteLine(" This backlog item is called " + _name + " and contains the activities: "+ _activities);}
    
    public string GetName()
    {
        return  _name;
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
    }

    public State.State GetState()
    {
        return _state;
    }
}