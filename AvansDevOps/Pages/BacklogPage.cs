using AvansDevOps.ProjectManagement;

namespace AvansDevOps.Pages;

public class BacklogPage
{
    

    public static void HandleEditBacklog(BacklogComposite backlog)
    {
        var exit = false;
        while (!exit)
        {
            Console.WriteLine("\nBacklog");
            Console.WriteLine("\nOptions:\n " +
                              "0. Return\n " +
                              "1. View backlog items\n " +
                              "2. Add backlog item\n " +
                              "3. Remove backlog item\n " +
                              "4. Edit backlog item\n " +
                              "5.\n ");
            String input = Console.ReadLine();
            switch (input)
            {
                case "0":
                    exit = true;
                    break;
                case "1":
                    backlog.Print();
                    break;
                case "2":
                    Console.WriteLine("Name: ");
                    String backlogItemName = Console.ReadLine();
                    
                    Console.WriteLine("Description: ");
                    String backlogItemDescription = Console.ReadLine();
                    
                    BacklogItemComposite backlogItem = new BacklogItemComposite(backlogItemName, backlogItemDescription);
                    backlog.Add(backlogItem);
                    break;   
                case "3":
                    backlog.Print();
                    Console.WriteLine("Enter the number of the item you want to remove: ");
                    int backlogItemIndexToRemove = (int.Parse(Console.ReadLine()) -1);
                    backlog.Remove(backlog.getBacklogItemByIndex(backlogItemIndexToRemove));
                    break;                
                case "4":
                    backlog.Print();
                    Console.WriteLine("Enter the number of the item you want to edit: ");
                    int backlogItemIndexToEdit = (int.Parse(Console.ReadLine()) -1);
                    HandleEditBacklogItem(backlog.getBacklogItemByIndex(backlogItemIndexToEdit));
                    break;
                case "5":
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again");
                    break;
            }
        }
    }
    
    
    

    private static void HandleEditBacklogItem(BacklogItemComposite backlogItem)
    {
        var exit = false;
        while (!exit)
        {
            Console.WriteLine("\nBacklog Item "  + backlogItem.GetName());
            Console.WriteLine("\nOptions:\n " +
                              "0. Return\n " +
                              "1. View activities\n " +
                              "2. Add activities\n " +
                              "3. Remove activities\n " +
                              "4. Edit activities\n " +
                              "5.\n ");
            String input = Console.ReadLine();
            switch (input)
            {
                case "0":
                    exit = true;
                    break;
                case "1":
                    backlogItem.Print();
                    break;
                case "2":
                    
                    break;   
                case "3":
                    break;                
                case "4":
                    
                    break;
                case "5":
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again");
                    break;
            }
        }
    }
}