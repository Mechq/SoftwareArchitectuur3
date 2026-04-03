using AvansDevOps.SprintFinish.Pipeline.Commands;
using AvansDevOps.SprintFinish.Pipeline.Commands.Analyse;
using AvansDevOps.SprintFinish.Pipeline.Commands.Build;
using AvansDevOps.SprintFinish.Pipeline.Commands.Test;

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