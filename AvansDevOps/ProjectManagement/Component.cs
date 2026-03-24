namespace AvansDevOps.ProjectManagement;

public interface Component
{
    public void Remove(Component component);
    public void Add(Component component);
    public void Print();
    
    public string GetName();
}