using AvansDevOps.Report;
using AvansDevOps.SprintFinish;
using AvansDevOps.SprintFinish.Pipeline;
using AvansDevOps.User;

namespace AvansDevOps.ProjectManagement.Composite;

public class SprintComposite : Component
{
    private readonly string _name;
    private readonly DateTime _startDate;
    private readonly DateTime _endDate;
    private ScrumMaster? _scrumMaster;
    public BacklogComposite Backlog =  new();
    private Boolean _isFinished;
    private Boolean _isReleased;
    private readonly ISprintStrategy _sprintStrategy;
    private readonly PipelineHandler _pipelineHandler;

    public SprintComposite(string name, DateTime startDate, DateTime endDate, ISprintStrategy sprintStrategy)
    {
        _name = name;
        _startDate = startDate;
        _endDate = endDate;
        _sprintStrategy = sprintStrategy;
        
        Pipeline pipeline = sprintStrategy switch
        {
            FeedbackSprintStrategy f => f.BuildPipeline(),
            ReleaseSprintStrategy r => r.BuildPipeline(),
            _ => throw new NotSupportedException("Please provide a valid sprint strategy")
        };
        _pipelineHandler = new PipelineHandler(pipeline);
    }

    public void Remove(Component component)
    {
        Console.WriteLine("Invalid method for this class");
    }

    public void Add(Component component)
    {
        Console.WriteLine("Invalid method for this class");
    }
    
    public void Print(){ Console.WriteLine($"Sprint {_name} started at {_startDate} and ended at {_endDate}. The scrum master is {_scrumMaster}" ); }
    
    public string GetName(){return _name;}
    
    public Boolean CanEdit()
    {
        if (_endDate <= DateTime.Now || _isFinished || _isReleased)
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
        if (CanEdit())
        {
            _scrumMaster = scrumMaster;
        }
        else
        {
            Console.WriteLine("You can't edit this sprint anymore. Please contact someone from the tech department.");
        }
        
        
    }
    
    public void GenerateReport(IReportStrategy strategy, bool includeHeader, bool includeFooter)
    {
        IReport report = new Report.Report(this);

        if (includeHeader)
        {
            report = new HeaderDecorator(report, "Avans", "logo.png");
        }

        if (includeFooter)
        {
            report = new FooterDecorator(report, DateOnly.FromDateTime(DateTime.Now), 1);
        }

        report.Generate(strategy);
    }

    public void RunPipeline()
    {
        if (CanEdit())
        {
            bool success = _sprintStrategy.StartPipeline(_pipelineHandler);

            if (_sprintStrategy is ReleaseSprintStrategy && success) //deployment released
                _isReleased = true;

            if (_sprintStrategy.IsFinished()) //summary uploaded or deployment success
            {
                _isFinished = true;
            }

            Console.WriteLine(_isReleased
                ? "Sprint released successfully!"
                : "Sprint pipeline failed or not a release sprint.");
        }
        else
        {
            Console.WriteLine("You can't edit this sprint anymore. Please contact someone from the tech department.");
        }
    }

}