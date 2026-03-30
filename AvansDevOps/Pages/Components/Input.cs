using System.Runtime.InteropServices.JavaScript;

namespace AvansDevOps.Pages.Components;

public class Input
{
    public static String AskQuestion(string question)
    {
        string? answer = "";
        while (String.IsNullOrEmpty(answer))
        {
            Console.WriteLine(question);
            answer = Console.ReadLine();
        }
        return (answer);
    }
}