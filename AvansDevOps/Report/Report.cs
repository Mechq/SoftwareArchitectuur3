using AvansDevOps.ProjectManagement;
using AvansDevOps.ProjectManagement.Composite;

namespace AvansDevOps.Report;

public class Report : IReport
{
    public SprintComposite Sprint { get; }
    public Report(SprintComposite sprint)
    {
        Sprint = sprint;
    }
    public void Generate(IReportStrategy strategy)
    {
        strategy.GenerateReport(this);
    }
   
}