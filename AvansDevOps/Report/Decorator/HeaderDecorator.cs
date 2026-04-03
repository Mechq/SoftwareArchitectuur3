namespace AvansDevOps.Report;

public class HeaderDecorator : ReportDecorator
{
    private string _companyName;
    private string _logoUrl;

    public HeaderDecorator(IReport inner, string companyName, string logoUrl) 
        : base(inner)
    {
        _companyName = companyName;
        _logoUrl = logoUrl;
    }

    public override void Generate(IReportStrategy strategy)
    {
        strategy.GenerateHeader(_companyName, _logoUrl);
        _inner.Generate(strategy);
    }
}