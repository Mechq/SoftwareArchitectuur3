using AvansDevOps.User;

namespace AvansDevOps.ProjectManagement;

public class ProjectComposite : Component
{
    private String _name;
    private ProductOwner _productOwner;
    private List<User.User> _users = [];
    private readonly List<SprintComposite> _sprints = [];
    public BacklogComposite backlog =  new BacklogComposite();

    public ProjectComposite(string name, ProductOwner productOwner)
    {
        _name = name;
        _productOwner = productOwner;
    }

    public void Remove(Component component)
    {
        if (component is not SprintComposite)
        {
            throw new ArgumentException("Component must be of type SprintComposite");
        }
        _sprints.Remove((SprintComposite) component);
        Console.WriteLine("Successfully removed Sprint: "  + component.GetName());

    }

    
    public void Add(Component component)
    {
        if (component is not SprintComposite)
        {
            throw new ArgumentException("Component must be of type SprintComposite");
        }
        _sprints.Add((SprintComposite) component);
        
        Console.WriteLine("Successfully added Sprint: "  + component.GetName());
    }

    public void Print(){Console.WriteLine("Project is called:" + _name);}

    public List<SprintComposite> GetSprints()
    {
        return _sprints;
    }

    public SprintComposite GetSprintByIndex(int index)
    {
        return  _sprints[index];
    }
    
    public string GetName()
    {
        return _name;
    }

    public void PrintSprints()
    {
        if (_sprints.Count == 0)
        {
            Console.WriteLine("No sprints found");
        }
        else
        {
            for(int i =0; i < _sprints.Count; i++)
            {
                Console.WriteLine($"{i+1}. " + _sprints[i].GetName());
            }
        }
    }

    public void AddUser(User.User user)
    {
        _users.Add(user);
        Console.WriteLine("Successfully added User: " + user.ToString());
    }

    public void RemoveUser(User.User user)
    {
        _users.Remove(user);
        Console.WriteLine("Successfully removed User: " + user.ToString());
    }

    public void PrintAllUsers()
    {
        foreach (User.User user in _users)
        {
            Console.WriteLine(user.ToString());
        }
    }
}