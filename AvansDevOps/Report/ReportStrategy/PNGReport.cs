namespace AvansDevOps.Report;

public class PNGReport : IReportStrategy
{
    public void GenerateReport(Report data)
    {
        Console.WriteLine("PNG Report");
        Console.WriteLine(data);
    }

    public void GenerateHeader(string companyName, string logoUrl)
    {
        Console.WriteLine($"Company: {companyName}");
        Console.WriteLine($"LogoUrl: {logoUrl}");
    }

    public void GenerateFooter(DateOnly date, int version)
    {
        Console.WriteLine($"Company: {date}");
        Console.WriteLine($"LogoUrl: {version}");
    }
}