using AvansDevOps;
using AvansDevOps.ProjectManagement;
using AvansDevOps.User;

internal class Program
{
    private static void Main()
    {
        Console.WriteLine("Login");

        Console.WriteLine("Email: ");
        var email = Console.ReadLine();

        Console.WriteLine("Password: ");
        var password = Console.ReadLine();

        var phoneNumber = 31622434;
        var userName = "Main User";
        var productOwner = new ProductOwner(userName, email, password, phoneNumber);

        var projects = new List<ProjectComposite>();

        Console.WriteLine("You are now logged in as a Product Owner");
        Console.WriteLine("You have " + projects.Count + " projects");

        var exit = false;
        while (!exit)
        {
            Console.WriteLine("\nOptions: \n 1. Create a project \n 2. View all your projects \n 3. Quit");
            Console.WriteLine("Enter option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    CreateProject(productOwner);
                    break;
                case "2":
                    if (projects.Count == 0) Console.WriteLine("You have no projects");
                    foreach (var project in projects) project.Print();
                    ;
                    break;
                case "3":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }


    protected static void CreateProject(ProductOwner productOwner)
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
                    var sprint = createSprint();
                    project.Add(sprint);
                    break;
                case "2":
                    project.PrintSprints();
                    break;
                case "3":
                    Console.WriteLine("Enter the name of your backlog item: ");
                    var backlogItemName = Console.ReadLine();
                    
                    BacklogItemComposite backlogItem = new BacklogItemComposite(backlogItemName);
                    project.backlog.Add(backlogItem);
                    break;
                case "4":
                    Console.WriteLine("What sprint do you want to edit? (type in the number)");
                    project.PrintSprints();
                    HandleEditSprint(Console.ReadLine(), project);
                    break;
                case "5":
                    //print project backlog (add method in project class)
                    project.backlog.Print();
                    break;
                case "6":
                    var user = createUser();
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
                case "q":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }

    private static SprintComposite createSprint()
    {
        Console.WriteLine("Sprint Name: ");
        var sprintName = Console.ReadLine();
        Console.WriteLine("How many days: ");
        var days = Console.ReadLine();
        var formatEndDate = DateTime.Today.AddDays(int.Parse(days));
        
        var sprint = new SprintComposite(sprintName, new DateTime(), formatEndDate);
        return sprint;
    }

    private static User createUser()
    {
        while (true)
        {
            Console.WriteLine("User Name: ");
            var username = Console.ReadLine();
            Console.WriteLine("Email: ");
            var email = Console.ReadLine();
            Console.WriteLine("Password: ");
            var password = Console.ReadLine();
            Console.WriteLine("Phonenumber: ");
            var phoneNumber = Console.ReadLine();
            Console.WriteLine("Role (1. Developer, 2. Scrum Master): ");
            var role = Console.ReadLine();
            User user;
            if (role == "1")
            {
                user = new Developer(username, email, password, int.Parse(phoneNumber));
                return user;
            }

            if (role == "2")
            {
                user = new ScrumMaster(username, email, password, int.Parse(phoneNumber));
                return user;
            }

            Console.WriteLine("Invalid option, please try again");
        }
    }


    private static void HandleEditSprint(string option, ProjectComposite project)
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
                              "4. Edit backlog(1,2,3,etc.)\n " +
                              "5.\n ");
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
                    HandleEditBacklog(sprint.backlog);
                    break;
                case "5":
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again");
                    break;
            }
        }
    }

    private static void HandleEditBacklog(BacklogComposite backlog)
    {
        
    }
}