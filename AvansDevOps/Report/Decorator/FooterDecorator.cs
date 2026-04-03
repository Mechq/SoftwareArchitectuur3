namespace AvansDevOps.Report;

public class FooterDecorator : ReportDecorator
{
    private DateOnly _date;
    private int _version;

    public FooterDecorator(IReport inner, DateOnly date, int version)
        : base(inner)
    {
        _date = date;
        _version = version;
    }

    public override void Generate(IReportStrategy strategy)
    {
        _inner.Generate(strategy);
        strategy.GenerateFooter(_date, _version);
    }
}