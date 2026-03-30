namespace AvansDevOps.Thread;

public interface IBacklogObserver
{
    public void update(State.State state);
    public String GetName();
    public List<String> GetMessages();
    public void AddMessage(String message);
}