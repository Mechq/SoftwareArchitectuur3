using System.Runtime.InteropServices.JavaScript;
using AvansDevOps.User;

namespace AvansDevOps.ProjectManagement;

public class SprintComposite : Component
{
    private string _name;
    private DateTime _startDate;
    private DateTime _endDate;
    private ScrumMaster _scrumMaster;
    public BacklogComposite  backlog;
    private Boolean _isFinished = false;
    private Boolean _isReleased = false;

    public SprintComposite(string name, DateTime startDate, DateTime endDate)
    {
        _name = name;
        _startDate = startDate;
        _endDate = endDate;
    }

    public void Remove(Component component)
    {
        Console.WriteLine("Invalid method for this class");
    }

    public void Add(Component component)
    {
        Console.WriteLine("Invalid method for this class");
    }

    public void Print(){ Console.WriteLine($"Sprint {_name} started at {_startDate} and ended at {_endDate}. The scrummaster is {_scrumMaster.ToString()}" ); }
    
    public string GetName(){return _name;}
    
    public Boolean CanEdit()
    {
        if (_endDate <= DateTime.Now)
        {
            return false;
        }
        return true;
    }

    public Boolean IsSuccessfulSprint()
    {
        return _isReleased;
    }

    public void AssignScrumMaster(ScrumMaster scrumMaster)
    {
        _scrumMaster = scrumMaster;
    }

    public void Edit()
    {
        Console.WriteLine("Edit name: ");
        this._name = Console.ReadLine();
        
        Console.WriteLine("How many days: ");
        var days = Console.ReadLine();
        _endDate = DateTime.Today.AddDays(int.Parse(days));
    }
}