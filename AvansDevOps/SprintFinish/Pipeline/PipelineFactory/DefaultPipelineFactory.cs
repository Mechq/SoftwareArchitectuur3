using AvansDevOps.SprintFinish.Pipeline.Commands;
using AvansDevOps.SprintFinish.Pipeline.Commands.Analyse;
using AvansDevOps.SprintFinish.Pipeline.Commands.Build;
using AvansDevOps.SprintFinish.Pipeline.Commands.Test;

namespace AvansDevOps.SprintFinish.Pipeline.PipelineFactory;

public class DefaultPipelineFactory : IPipelineToolFactory
{
    public GetSources CreateSourceAction() => new GetSources();
    public PackageInstaller CreatePackageAction() => new PackageInstaller();
    public IBuildStrategy CreateBuildAction() => new DOTNET();
    public ITestStrategy CreateTestAction() => new XUnit();
    public IAnalyseTemplate CreateAnalyseAction() => new SonarQube();
    public Deploy CreateDeploymentAction() => new Deploy();
    public Utility CreateUtilityAction() => new Utility();
}