using AvansDevOps.Notification;
using AvansDevOps.SprintFinish.Pipeline.Commands;

namespace AvansDevOps.SprintFinish.Pipeline;

public class Pipeline
{
    private readonly List<ICommand> _commands = [];
    private readonly List<Log> _logs = [];
    private readonly INotification _notifier = new EmailAdapter(new EmailService());

    public void AddCommand(ICommand command)
    {
        _commands.Add(command);
    }
    
    public bool Execute()
    {
        
        foreach (var command in _commands)
        {
            DateTime dt = DateTime.Now;
            try
            {
                command.Execute();
                _logs.Add(new Log($"{command.GetType().Name}", "SUCCESS", dt));
                
            }
            catch (Exception e)
            {
                _logs.Add(new Log($"{command.GetType().Name}", "FAIL", dt));
                _notifier.SendNotification(e.Message);
                throw;
            }
        }

        return true;
    }

    public List<Log> GetLogs()
    {
        return _logs;
    }
    
}