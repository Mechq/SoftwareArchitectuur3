using System.Runtime.InteropServices.JavaScript;
using AvansDevOps.User;

namespace AvansDevOps.ProjectManagement;

public class SprintComposite : Component
{
    private string _name;
    private DateTime _startDate;
    private DateTime _endDate;
    private ScrumMaster _scrumMaster;
    private SprintBacklogComposite  _backlog;
    private Boolean _isFinished = false;
    private Boolean _isReleased = false;

    public SprintComposite(string name, DateTime startDate, DateTime endDate,SprintBacklogComposite backlog)
    {
        _name = name;
        _startDate = startDate;
        _endDate = endDate;
        _backlog = backlog;
    }

    public void Remove(Component component)
    {
        
    }

    public void Add(Component component)
    {
        
    }

    public void Print(){ Console.WriteLine(_name); }
    
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
}