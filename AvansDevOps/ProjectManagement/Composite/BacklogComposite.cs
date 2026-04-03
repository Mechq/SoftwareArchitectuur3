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
        _backlogItems.Remove((BacklogItemComposite)backlogItem);
    }
    
    public List<BacklogItemComposite> GetBacklogItems()
    {
        return _backlogItems;
    }
    
    public void Print()
    {
        foreach (BacklogItemComposite backlogItem in _backlogItems)
        {
            Console.WriteLine($"{_backlogItems.IndexOf(backlogItem) +1}. Item : {backlogItem.GetName()}");
        }
    }

    public String GetName()
    {
        return "Backlog";
    }
    
    
    public void MoveBacklogItem(BacklogItemComposite item, BacklogComposite destination)
    {
        if (!_backlogItems.Contains(item))
        {
            Console.WriteLine("Item not found in this backlog.");
            return;
        }
        Remove(item);
        destination.Add(item);
    }
}