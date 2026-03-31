using AvansDevOps;
using AvansDevOps.Pages;
using AvansDevOps.Pages.Components;
using AvansDevOps.ProjectManagement;
using AvansDevOps.Thread;
using AvansDevOps.User;

internal class Program
{
    private static void Main()
    {
        Console.WriteLine("Login");
        var email = Input.AskQuestion("Email: ");
        var password = Input.AskQuestion("Password: ");

        var phoneNumber = 31622434;
        var userName = "Stef Rensma";
        var productOwner = new ProductOwner(userName, email, password, phoneNumber);

        var projects = new List<ProjectComposite>();
        
        Console.WriteLine("You are now logged in as " + userName);
        Console.WriteLine("You have " + projects.Count + " projects");

        var exit = false;
        while (!exit)
        {
            Console.WriteLine("\nOptions: \n 0. Quit \n 1. Create a project \n 3. View project");
            String option = Input.AskQuestion("What would you like to do?");

            switch (option)
            {
                case "0":
                    exit = true;
                    break;
                case "1":
                    ProjectPage.CreateProject(productOwner, null);
                    
                    break;
                case "2":
                    if (projects.Count == 0) Console.WriteLine("You have no projects");
                    foreach (var project in projects) project.Print();
                    ;
                    break;
                case "3":
                    ProjectPage.CreateProject(productOwner, SeedData(productOwner));
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }

    private static ProjectComposite SeedData(ProductOwner productOwner)
    {

        ProjectComposite project = new ProjectComposite("Portfolio", productOwner);
        Console.WriteLine("Created project");
        BacklogItemComposite backlogItemOne = new BacklogItemComposite("Diagram maken", "UML klassen Diagram tekenen.");
        project.backlog.Add(backlogItemOne);
        BacklogItemComposite backlogItemTwo = new BacklogItemComposite("Github repository aanmaken",
            "Maak een GitHub repoistory voor de frontend en backend.");
        project.backlog.Add(backlogItemTwo);
        SprintComposite sprintOne = new SprintComposite("Week 1, Startup", DateTime.Now, new DateTime().AddDays(7));
        SprintComposite sprintTwo =
            new SprintComposite("Week 2, Code grind", new DateTime().AddDays(7), new DateTime().AddDays(14));
        project.Add(sprintOne);
        project.Add(sprintTwo);
        BacklogItemComposite sprintBacklogItemOne = new BacklogItemComposite("Onderzoek naar techstack",
            "Ga op onderzoek om te kijken wat de beste techstack voor de frontend en backend is");
        BacklogItemComposite sprintBacklogItemTwo =
            new BacklogItemComposite("Doe software engineering", "Voer de eerste drie stappen van het SDLC uit");
        sprintTwo.backlog.Add(sprintBacklogItemOne);
        sprintTwo.backlog.Add(sprintBacklogItemTwo);
        ActivityLeaf activityOne =
            new ActivityLeaf("Frontend studie", "Kijk naar de beste technologieen voor web applicaties");
        ActivityLeaf activityTwo =
            new ActivityLeaf("Backend studie", "Kijk naar de beste technologieen voor backend servers");
        sprintBacklogItemOne.Add(activityOne);
        sprintBacklogItemOne.Add(activityTwo);
        Discussion discussionOne = new Discussion("React vs Angular for frontend",
            new List<string> {"I think react is wayyy better then Angular", "No, Angular is so much more optimized!"});
        sprintBacklogItemOne.AddObserver(discussionOne);
    return project;

    }


    
}