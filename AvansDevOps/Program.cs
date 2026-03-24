using System.Net.Quic;
using AvansDevOps.ProjectManagement;
using AvansDevOps.User;

class Program
{

    static void Main()
    {
        Console.WriteLine("Login");
        
        Console.WriteLine("Email: ");
        string email = Console.ReadLine();
        
        Console.WriteLine("Password: ");
        string password = Console.ReadLine();
        
        int phoneNumber = 31622434;
        string userName = "Main User";
        ProductOwner productOwner = new ProductOwner(userName, email, password,  phoneNumber);
        
        List<ProjectComposite> projects = new List<ProjectComposite>();
        
        Console.WriteLine("You are now logged in as a Product Owner");
        Console.WriteLine("You have " + projects.Count + " projects");
        
        Boolean exit = false;
        while (!exit)
        {
            Console.WriteLine("\nOptions: \n 1. Create a project \n 2. View all your projects \n 3. Quit");
            Console.WriteLine("Enter option: ");
            String option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    CreateProject(productOwner);
                    break;
                case "2":
                    if (projects.Count == 0)
                    {
                        Console.WriteLine("You have no projects");
                    }
                    foreach (ProjectComposite project in projects)
                    {
                        project.Print();
                    };
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
        List<User> users = new List<User>();
        users.Add(productOwner);
        
        Console.WriteLine("Please enter your project name: ");
        String projectName = Console.ReadLine();
        
        ProjectBacklogComposite projectBacklog = new ProjectBacklogComposite();
        ProjectComposite project = new ProjectComposite(projectName, productOwner, projectBacklog);
        
        Boolean exit =  false; 
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
                              "Q. Quit "  );
            Console.WriteLine("Enter option:");
            String input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    SprintComposite sprint = createSprint();
                    project.Add(sprint);
                    break;
                case "2":
                    project.PrintSprints();
                    break;
                case "3":
                    Console.WriteLine("Enter the name of your backlog item: ");
                    String backlogItem = Console.ReadLine();
                    BacklogItemComposite activity = new BacklogItemComposite(backlogItem);
                    projectBacklog.Add(activity);
                    break;
                case "4":
                    //TODO
                    break;
                case "5":
                    projectBacklog.Print();
                    break;
                case "6":
                    User user = createUser();
                    users.Add(user);
                    Console.WriteLine("Successfully added: " + user.GetType() + " " + user.ToString());
                    break;
                case "7":
                    Developer dummyDeveloper = new Developer("Stef Rensma", "ss.rensma@student.avans.nl", "wachtwoord123", 31123456);
                    ScrumMaster dummyScrumMaster = new ScrumMaster("Menno Emmerik", "m2.emmerik@student.avans.nl", "wachtwoord123", 317890123);
                    users.Add(dummyDeveloper);
                    users.Add(dummyScrumMaster);
                    Console.WriteLine("Successfully added 2 users");
                    break;
                case "8":
                    if (users.Count == 0)
                    {
                        Console.WriteLine("You have no users");
                    }
                    foreach (User _user in users)
                    {
                        Console.WriteLine(_user.ToString());
                    };
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
        String sprintName = Console.ReadLine();
        Console.WriteLine("How many days: ");
        String days = Console.ReadLine();
        DateTime formatEndDate = DateTime.Today.AddDays(int.Parse(days));
                    
        SprintBacklogComposite sprintBacklog = new SprintBacklogComposite();
        SprintComposite sprint = new SprintComposite(sprintName, new DateTime(), formatEndDate, sprintBacklog);
        return sprint;
    }

    private static User createUser()
    {
        while (true)
        {
            Console.WriteLine("User Name: ");
            String username = Console.ReadLine();
            Console.WriteLine("Email: ");
            String email = Console.ReadLine();
            Console.WriteLine("Password: ");
            String password = Console.ReadLine();
            Console.WriteLine("Phonenumber: ");
            String phoneNumber = Console.ReadLine();
            Console.WriteLine("Role (1. Developer, 2. Scrum Master): ");
            String role = Console.ReadLine();
            User user;
            if (role == "1")
            {
                user = new Developer(username, email, password, int.Parse(phoneNumber));
                return user; }
            if (role == "2")
            {
                user = new ScrumMaster(username, email, password, int.Parse(phoneNumber));
                return user;
            }
            Console.WriteLine("Invalid option, please try again");
            
        }
    }
}
