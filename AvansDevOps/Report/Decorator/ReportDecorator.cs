namespace AvansDevOps.Report;

public abstract class ReportDecorator : IReport
{
    protected IReport _inner;
    protected ReportDecorator(IReport inner) { _inner = inner; }
    public abstract void Generate(IReportStrategy strategy);
}