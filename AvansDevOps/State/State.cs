using AvansDevOps.ProjectManagement;

namespace AvansDevOps.State;

public abstract class State

{
    protected Component _component; 
    protected State(Component component)
    {
        _component = component;
    }
    
    public abstract void SendNotification(String info);
    public virtual void TaskComplete() {}
    public virtual void StartOver() {}
    public virtual void StartedTesting() {}
    public virtual void Validated() {}
    public virtual void Invalidated() {}
    public virtual void CompletedTests() {}
    public virtual void FailedTests() {}
    public virtual void StartedWorking() {}
}