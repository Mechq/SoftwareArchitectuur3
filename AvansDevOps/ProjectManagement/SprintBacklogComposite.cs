namespace AvansDevOps.ProjectManagement;

public class SprintBacklogComposite : Component
{
    private List<Component> _items;
    
    
    public void Remove(Component component)
    {
        if (component is not BacklogItemComposite)
        {
            throw new ArgumentException("Component must be of type BacklogItemComposite");
        }
        _items.Remove(component);
    }

    public void Add(Component component)
    {
        if (component is not BacklogItemComposite)
        {
            throw new ArgumentException("Component must be of type BacklogItemComposite");
        }
        _items.Add(component);
    }

    public void Print(){}

    public string GetName()
    {
        return "This is a backlog";
    }

    public Boolean CanEdit()
    {
        return true;
    }
}