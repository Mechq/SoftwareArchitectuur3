using AvansDevOps.ProjectManagement;

namespace AvansDevOps.Pages;

public class SprintPage
{
    

    public static SprintComposite CreateSprint()
    {
        Console.WriteLine("Sprint Name: ");
        var sprintName = Console.ReadLine();
        Console.WriteLine("How many days: ");
        var days = Console.ReadLine();
        var formatEndDate = DateTime.Today.AddDays(int.Parse(days));
        
        var sprint = new SprintComposite(sprintName, new DateTime(), formatEndDate);
        return sprint;
    }


    public static void HandleEditSprint(string option, ProjectComposite project)
    {
        SprintComposite sprint = project.GetSprintByIndex(int.Parse(option) - 1);
        
        var exit = false;
        while (!exit)
        {
            Console.WriteLine("\nSprint Name: " + sprint.GetName());
            Console.WriteLine("\nOptions:\n " +
                              "0. Return\n " +
                              "1. View sprint\n " +
                              "2. Edit sprint (name & date)\n " +
                              "3. View sprint backlog\n " +
                              "4. Edit sprint backlog\n " +
                              "5. Assign a scrum master\n ");
            String input = Console.ReadLine();
            switch (input)
            {
                case "0":
                    exit = true;
                    break;
                case "1":
                    sprint.Print();
                    break;
                case "2":
                    sprint.Edit();
                    break;   
                case "3":
                    sprint.backlog.Print();
                    break;                
                case "4":
                    BacklogPage.HandleEditBacklog(sprint.backlog);
                    break;
                case "5":
                    project.PrintAllUsers();
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again");
                    break;
            }
        }
    }
}