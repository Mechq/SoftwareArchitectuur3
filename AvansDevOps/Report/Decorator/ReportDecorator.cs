namespace AvansDevOps.Report;

public abstract class ReportDecorator : IReport
{
    protected  IReport Inner;
    protected ReportDecorator(IReport inner) { Inner = inner; }
    public abstract void Generate(IReportStrategy strategy);
}