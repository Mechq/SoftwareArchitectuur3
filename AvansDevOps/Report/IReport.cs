namespace AvansDevOps.Report;

public interface IReport
{
    void Generate(IReportStrategy strategy);
}