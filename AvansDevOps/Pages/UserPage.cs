using AvansDevOps.User;

namespace AvansDevOps.Pages;

public class UserPage
{
    public static User.User CreateUser()
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
            User.User user;
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
}