using AvansDevOps.User;

namespace AvansDevOps.ProjectManagement;

public class ProjectComposite : Component
{
    private String _name;
    private ProductOwner _productOwner;
    private readonly List<Component> _sprints = [];
    private ProjectBacklogComposite _backlog;

    public ProjectComposite(string name, ProductOwner productOwner, ProjectBacklogComposite backlog)
    {
        _name = name;
        _productOwner = productOwner;
        _backlog = backlog;
    }

    public void Remove(Component component)
    {
        if (component is not SprintComposite)
        {
            throw new ArgumentException("Component must be of type SprintComposite");
        }
        _sprints.Remove(component);
        Console.WriteLine("Successfully removed Sprint: "  + component.GetName());

    }

    
    public void Add(Component component)
    {
        if (component is not SprintComposite)
        {
            throw new ArgumentException("Component must be of type SprintComposite");
        }
        _sprints.Add(component);
        
        Console.WriteLine("Successfully added Sprint: "  + component.GetName());
    }

    public void Print(){Console.WriteLine("Project is called:" + _name);}

    public List<Component> GetSprints()
    {
        return _sprints;
    }
    
    public ProjectBacklogComposite GetProjectBacklog()
    {
        return _backlog;
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
            foreach (Component component in _sprints)
            {
                component.Print();
            }
        }
    }
}