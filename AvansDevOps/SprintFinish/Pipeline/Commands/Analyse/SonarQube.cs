namespace AvansDevOps.SprintFinish.Pipeline.Commands.Analyse;

public class SonarQube : IAnalyseTemplate
{
    protected override void AnalysisPreparation()
    {
        Console.WriteLine("Analysis Preparation in SonarQube");
    }

    protected override void AnalysisExecution()
    {
        Console.WriteLine("Analysis Execution in SonarQube");
    }

    protected override void AnalysisReporting()
    {
        Console.WriteLine("Analysis Reporting in SonarQube");
    }
}