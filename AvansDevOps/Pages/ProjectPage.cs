using AvansDevOps.ProjectManagement;
using AvansDevOps.User;

namespace AvansDevOps.Pages;

public class ProjectPage
{
    public static void CreateProject(ProductOwner productOwner)
    {
        Console.WriteLine("Please enter your project name: ");
        var projectName = Console.ReadLine();
        
        var project = new ProjectComposite(projectName, productOwner);
        project.AddUser(productOwner);

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
            Console.WriteLine("Enter option:");
            var input = Console.ReadLine();
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
                    Console.WriteLine("Enter the name of your backlog item: ");
                    var backlogItemName = Console.ReadLine();
                    
                    Console.WriteLine("Enter the description of your backlog item: ");
                    var backlogItemDescription = Console.ReadLine();
                    
                    BacklogItemComposite backlogItem = new BacklogItemComposite(backlogItemName, backlogItemDescription);
                    project.backlog.Add(backlogItem);
                    break;
                case "4":
                    Console.WriteLine("What sprint do you want to edit? (type in the number)");
                    project.PrintSprints();
                    SprintPage.HandleEditSprint(Console.ReadLine(), project);
                    break;
                case "5":
                    //print project backlog (add method in project class)
                    project.backlog.Print();
                    break;
                case "6":
                    var user = UserPage.CreateUser();
                    project.AddUser(user);
                    break;
                case "7":
                    var dummyDeveloper = new Developer("Stef Rensma", "ss.rensma@student.avans.nl", "wachtwoord123",
                        31123456);
                    var dummyScrumMaster = new ScrumMaster("Menno Emmerik", "m2.emmerik@student.avans.nl",
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