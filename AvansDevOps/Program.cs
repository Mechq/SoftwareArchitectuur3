using AvansDevOps;
using AvansDevOps.Pages;
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
        var userName = "Stef Rensma";
        var productOwner = new ProductOwner(userName, email, password, phoneNumber);

        var projects = new List<ProjectComposite>();

        Console.WriteLine("You are now logged in as " + userName);
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
                    ProjectPage.CreateProject(productOwner);
                    
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


    
}