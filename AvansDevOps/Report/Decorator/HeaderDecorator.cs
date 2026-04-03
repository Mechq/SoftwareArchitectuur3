namespace AvansDevOps.Report;

public class HeaderDecorator : ReportDecorator
{
    private readonly string _companyName;
    private readonly string _logoUrl;

    public HeaderDecorator(IReport inner, string companyName, string logoUrl) 
        : base(inner)
    {
        _companyName = companyName;
        _logoUrl = logoUrl;
    }

    public override void Generate(IReportStrategy strategy)
    {
        strategy.GenerateHeader(_companyName, _logoUrl);
        Inner.Generate(strategy);
    }
}