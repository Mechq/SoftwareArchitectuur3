using AvansDevOps.State;
using AvansDevOps.User;

namespace AvansDevOps.ProjectManagement;

public class ActivityLeaf : Component
{
    private String _name;
    private String _description;
    private State.State _state;
    private Developer _developer;
    
    public ActivityLeaf(string name, string description)
    {
        _name = name;
        _description = description;

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
    }

    public State.State GetState()
    {
        return _state;
    }
    
}