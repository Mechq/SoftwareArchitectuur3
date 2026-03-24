using AvansDevOps.State;
using AvansDevOps.User;

namespace AvansDevOps.ProjectManagement;

public class ActivityLeaf : Component
{
    private String _name;
    private String _description;
    private IState status;
    private Developer _developer;
    
    public ActivityLeaf(string name, string description, Developer developer)
    {
        _name = name;
        _description = description;
        _developer = developer;
    }

    public void Remove(Component component) {}

    public void UpdateDescription(String description)
    {
        //optionally add checks
        _description = description;
    }
    
    public void Add(Component component) {}

    public void Print()
    {
        Console.WriteLine($"{_name} - {_description}");
    }
    
    public string GetName()
    {
        return _name;
    }

    public void ChangeState(IState newState)
    {
        status = newState;
    }

    public IState GetStatus()
    {
        return status;
    }
}