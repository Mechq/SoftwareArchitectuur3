using AvansDevOps.Pages.Components;
using AvansDevOps.ProjectManagement;
using AvansDevOps.User;

namespace AvansDevOps.Pages;

public class ProjectPage
{
    public static void CreateProject(ProductOwner productOwner, ProjectComposite? project)
    {
        if (project == null)
        {
            var projectName = Input.AskQuestion("Please enter your project name: ");
            project = new ProjectComposite(projectName, productOwner);
            project.AddUser(productOwner);
        }
        

        var exit = false;
        while (!exit)
        {
            Console.WriteLine("\nProject: " + project.GetName());

            Console.WriteLine("\nOptions: \n " +
                              "0. Return \n " +
                              "1. Add a sprint \n " +
                              "2. View all sprints \n " +
                              "3. Add backlog item to project backlog \n " +
                              "4. Edit a sprint \n " +
                              "5. View all backlog items \n " +
                              "6. Create A user \n " +
                              "7. Add dummy users \n " +
                              "8. View all users \n " +
                              "Q. Quit ");
            var input = Input.AskQuestion("Enter option: ");
            switch (input)
            {
                case "1":
                    var sprint = SprintPage.CreateSprint();
                    project.Add(sprint);
                    break;
                case "2":
                    project.PrintSprints();
                    break;
                case "3":
                    var backlogItemName = Input.AskQuestion("Enter the name of your backlog item: ");
                    var backlogItemDescription = Input.AskQuestion("Enter the description of your backlog item: ");
                    
                    BacklogItemComposite backlogItem = new BacklogItemComposite(backlogItemName, backlogItemDescription);
                    project.backlog.Add(backlogItem);
                    break;
                case "4":
                    Console.WriteLine("What sprint do you want to edit? (type in the number)");
                    project.PrintSprints();
                    SprintPage.HandleEditSprint(Console.ReadLine(), project);
                    break;
                case "5":
                    project.backlog.Print();
                    break;
                case "6":
                    var user = UserPage.CreateUser();
                    project.AddUser(user);
                    break;
                case "7":
                    Developer dummyDeveloper = new Developer("Stef Rensma", "ss.rensma@student.avans.nl", "wachtwoord123",
                        31123456);
                    ScrumMaster dummyScrumMaster = new ScrumMaster("Menno Emmerik", "m2.emmerik@student.avans.nl",
                        "wachtwoord123", 317890123);
                    project.AddUser(dummyDeveloper);
                    project.AddUser(dummyScrumMaster);
                    break;
                case "8":
                    project.PrintAllUsers();
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }
}