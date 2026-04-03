using AvansDevOps.SprintFinish.Pipeline.Build;
using AvansDevOps.SprintFinish.Pipeline.Test;

namespace AvansDevOps.SprintFinish.Pipeline;

public interface IPipelineToolFactory
{
    GetSources CreateSourceAction();
    PackageInstaller CreatePackageAction();
    IBuildStrategy CreateBuildAction();
    ITestStrategy CreateTestAction();
    IAnalyseTemplate CreateAnalyseAction();
    Deploy CreateDeploymentAction();
    Utility CreateUtilityAction();
}