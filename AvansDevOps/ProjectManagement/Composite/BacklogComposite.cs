using AvansDevOps.ProjectManagement;

namespace AvansDevOps;

public class BacklogComposite : Component
{
    private List<BacklogItemComposite> _backlogItems = [];
    
    public void Add(Component backlogItem)
    {
        _backlogItems.Add((BacklogItemComposite)backlogItem);
    }
    
    public void Remove(Component backlogItem)
    {
        _backlogItems.Add((BacklogItemComposite)backlogItem);
    }
    
    public void Print()
    {
        foreach (BacklogItemComposite backlogItem in _backlogItems)
        {
            Console.WriteLine($"{_backlogItems.IndexOf(backlogItem)}. Item : {backlogItem.GetName()}");
        }
    }

    public String GetName()
    {
        return "Backlog";
    }
}