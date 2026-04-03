namespace AvansDevOps.SprintFinish.Pipeline;

public class PipelineBuilder
{
    private readonly Pipeline _pipeline = new();
    private readonly IPipelineToolFactory _factory;

    public PipelineBuilder(IPipelineToolFactory factory)
    {
        _factory = factory;
    }

    public PipelineBuilder AddSourceStep()
    {
        _pipeline.AddCommand(_factory.CreateSourceAction());
        return this;
    }

    public PipelineBuilder AddPackageStep()
    {
        _pipeline.AddCommand(_factory.CreatePackageAction());
        return this;
    }

    public PipelineBuilder AddBuildStep()
    {
        _pipeline.AddCommand(_factory.CreateBuildAction());
        return this;
    }

    public PipelineBuilder AddTestStep()
    {
        _pipeline.AddCommand(_factory.CreateTestAction());
        return this;
    }

    public PipelineBuilder AddAnalyseStep()
    {
        _pipeline.AddCommand(_factory.CreateAnalyseAction());
        return this;
    }

    public PipelineBuilder AddDeployStep()
    {
        _pipeline.AddCommand(_factory.CreateDeploymentAction());
        return this;
    }

    public PipelineBuilder AddUtilityStep()
    {
        _pipeline.AddCommand(_factory.CreateUtilityAction());
        return this;
    }

    public Pipeline GetResult()
    {
       return  _pipeline;
    }
}