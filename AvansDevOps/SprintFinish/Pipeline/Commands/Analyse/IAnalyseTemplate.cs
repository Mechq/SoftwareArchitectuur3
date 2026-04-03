namespace AvansDevOps.SprintFinish.Pipeline.Commands.Analyse;

public abstract class IAnalyseTemplate : ICommand
{
    protected abstract void AnalysisPreparation();
    protected abstract void AnalysisExecution();
    protected abstract void AnalysisReporting();

    public void Execute()
    {
        AnalysisPreparation();
        AnalysisExecution();
        AnalysisReporting();
    }
}