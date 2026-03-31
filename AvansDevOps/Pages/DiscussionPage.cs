using AvansDevOps.Pages.Components;
using AvansDevOps.ProjectManagement;
using AvansDevOps.Thread;

namespace AvansDevOps.Pages;

public class DiscussionPage
{
    public static void HandleDiscussion(BacklogItemComposite backlogItem, String discussionIndex)
    {
        
        IBacklogObserver discussion = backlogItem.GetObserverByIndex(int.Parse(discussionIndex));
        
        var exit = false;
        while (!exit)
        {
            Console.WriteLine("\nDiscussion for: " + backlogItem.GetName() + " and is about: " + discussion.GetName());
            Console.WriteLine("\nOptions:\n " +
                              "0. Return\n " +
                              "1. View messages\n " +
                              "2. Write a message\n ");
            String input = Input.AskQuestion("What would you like to do?");
            switch (input)
            {
                case "0":
                    exit = true;
                    break;
                case "1":
                    List<String> messages = discussion.GetMessages();
                    foreach (String message in messages)
                    {
                        Console.WriteLine(message);
                    }
                    break;
                case "2":
                    discussion.AddMessage(Input.AskQuestion("Write your message: "));
                    break;   
                default:
                    Console.WriteLine("Invalid option, please try again");
                    break;
            }
        }
    }
}