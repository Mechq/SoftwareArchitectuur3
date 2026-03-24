using AvansDevOps.User;

namespace AvansDevOps.ProjectManagement;

public class BacklogItemComposite : Component
{
    private string _name;
    private List<Component> _activities { get; }
    private Developer _developer;
    //private IState status;
    //private List<Observer> _observers;
    public BacklogItemComposite(string name)
    {
        _name = name;
        
    }

    public void Remove(Component component)
    {
        if (component is not ActivityLeaf)
        {
            throw new ArgumentException("Component must be of type ActivityLeaf");
        }
        _activities.Remove(component);
    }

    public void Add(Component component)
    {
        if (component is not ActivityLeaf)
        {
            throw new ArgumentException("Component must be of type ActivityLeaf");
        }
        _activities.Add(component);
    }

    public void Print(){Console.WriteLine(" This backlog item is called " + _name + " and contains the activities: "+ _activities);}
    
    public string GetName()
    {
        return  _name;
    }

    public void AssignDeveloper(Developer developer)
    {
        _developer = developer;
    }
}