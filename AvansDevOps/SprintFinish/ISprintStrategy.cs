using AvansDevOps.Notification;
using AvansDevOps.SprintFinish.Pipeline;

namespace AvansDevOps.SprintFinish;

public interface ISprintStrategy
{
    bool StartPipeline(PipelineHandler pipelineHandler);
    void FinishSprint();
    bool IsFinished();
}