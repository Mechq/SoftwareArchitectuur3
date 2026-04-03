namespace AvansDevOps.Report;

public class FooterDecorator : ReportDecorator
{
    private readonly DateOnly _date;
    private readonly int _version;

    public FooterDecorator(IReport inner, DateOnly date, int version)
        : base(inner)
    {
        _date = date;
        _version = version;
    }

    public override void Generate(IReportStrategy strategy)
    {
        Inner.Generate(strategy);
        strategy.GenerateFooter(_date, _version);
    }
}