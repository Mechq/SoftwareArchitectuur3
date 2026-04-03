using AvansDevOps.SprintFinish.Pipeline.Build;
using AvansDevOps.SprintFinish.Pipeline.Test;

namespace AvansDevOps.SprintFinish.Pipeline;

public class DefaultPipelineFactory : IPipelineToolFactory
{
    public GetSources CreateSourceAction() => new GetSources();
    public PackageInstaller CreatePackageAction() => new PackageInstaller();
    public IBuildStrategy CreateBuildAction() => new DOTNET();
    public ITestStrategy CreateTestAction() => new NUnit();
    public IAnalyseTemplate CreateAnalyseAction() => new SonarQube();
    public Deploy CreateDeploymentAction() => new Deploy();
    public Utility CreateUtilityAction() => new Utility();
}