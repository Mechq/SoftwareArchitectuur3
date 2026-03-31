using AvansDevOps.State;

namespace AvansDevOps.Thread;

public class Discussion : IBacklogObserver
{
    private String name;
    private List<String> messages = [];
    private bool canEdit = true;

    public Discussion(String name, List<String> messages)
    {
        this.name = name;
        this.messages = messages;
    }

    public void AddMessage(String message)
    {
        if (canEdit)
        {
            messages.Add(message);
        }
        else
        {
            Console.WriteLine("This discussion is closed");
        }
    }

    public List<String> GetMessages()
    {
        return messages;
    }

    public String GetName()
    {
        return name;
    } 
    
    public void update(State.State state)
    {
        canEdit = state.GetType() != typeof(State.DoneState); //done == no edit
    }

 
}