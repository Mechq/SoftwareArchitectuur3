namespace AvansDevOps.ProjectManagement;

public class ProjectBacklogComposite : Component
{
    private readonly List<Component> _items = [];

    public void Remove(Component component)
    {
        if (component is not BacklogItemComposite)
        {
            throw new ArgumentException("Component must be of type BacklogItemComposite");
        }
        _items.Remove(component);
        Console.WriteLine("Successfully removed BacklogItem");
    }

    public void Add(Component component)
    {
        if (component is not BacklogItemComposite)
        {
            throw new ArgumentException("Component must be of type BacklogItemComposite");
        }
        _items.Add(component);
        Console.WriteLine("Successfully added BacklogItem");
    }

    public void Print()
    {
        foreach (Component item in _items)
        {item.Print();}
    }
    
    public string GetName()
    {
        return "";
    }
}