using AvansDevOps.Pages.Components;
using AvansDevOps.ProjectManagement;
using AvansDevOps.User;

namespace AvansDevOps.Pages;

public class SprintPage
{
    

    public static SprintComposite CreateSprint()
    {
        var sprintName = Input.AskQuestion("Sprint Name: ");
        var days = Input.AskQuestion("How many days: ");
        var formatEndDate = DateTime.Today.AddDays(int.Parse(days));
        
        var sprint = new SprintComposite(sprintName, DateTime.Now, formatEndDate);
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
            String input = Input.AskQuestion("What would you like to do?");
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
                    project.PrintAllScrumMasters();
                    String userMail = Input.AskQuestion("Which scrum master do you want to assign? Please type in their mail address: ");
                    User.User? user  = project.GetUserByEmail(userMail);
                    if (user == null || user.GetType() != typeof(ScrumMaster) )
                    {
                        Console.WriteLine("Incorrect role/name");
                    }
                    else
                    {
                        sprint.AssignScrumMaster((ScrumMaster)user);
                    }
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again");
                    break;
            }
        }
    }
}