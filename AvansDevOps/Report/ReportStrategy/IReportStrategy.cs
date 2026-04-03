namespace AvansDevOps.Report;

public interface IReportStrategy
{
    void GenerateReport(Report data);
    void GenerateHeader(string companyName, string logoUrl);
    void GenerateFooter(DateOnly date, int version);
}