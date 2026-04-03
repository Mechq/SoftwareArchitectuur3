namespace AvansDevOps.SprintFinish.Pipeline;

public class Pipeline
{
    private readonly List<ICommand> _commands = [];
    private readonly List<Log> _logs = [];

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
                throw;
                return false;
            }
        }

        return true;
    }

    public List<Log> GetLogs()
    {
        return _logs;
    }
    
}