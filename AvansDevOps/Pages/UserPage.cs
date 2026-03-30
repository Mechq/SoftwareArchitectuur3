using AvansDevOps.Pages.Components;
using AvansDevOps.User;

namespace AvansDevOps.Pages;

public class UserPage
{
    public static User.User CreateUser()
    {
        while (true)
        {

            var username = Input.AskQuestion("User Name: ");
            var email = Input.AskQuestion("Email: ");
            var password = Input.AskQuestion("Password: ");
            var phoneNumber = Input.AskQuestion("Phone number: ");
            var role = Input.AskQuestion("Role (1. Developer, 2. Scrum Master): ");
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