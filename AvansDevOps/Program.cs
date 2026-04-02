using AvansDevOps;

using AvansDevOps.ProjectManagement;
using AvansDevOps.Thread;
using AvansDevOps.User;

internal class Program
{

    private static void Main()
    {
        var productOwner = new ProductOwner("Stef Rensma", "stef@gmail.com", "123", 31622434);
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
        sprintOne.backlog.Add(sprintBacklogItemOne);
        sprintOne.backlog.Add(sprintBacklogItemTwo);
        
        ActivityLeaf activityOne =
            new ActivityLeaf("Frontend studie", "Kijk naar de beste technologieen voor web applicaties");
        ActivityLeaf activityTwo =
            new ActivityLeaf("Backend studie", "Kijk naar de beste technologieen voor backend servers");
        sprintBacklogItemOne.Add(activityOne);
        sprintBacklogItemOne.Add(activityTwo);
        Discussion discussionOne = new Discussion("React vs Angular for frontend",
            new List<string> {"I think react is wayyy better then Angular", "No, Angular is so much more optimized!"});
        sprintBacklogItemOne.AddObserver(discussionOne);
        sprintBacklogItemOne.PrintObservers();
    }


    
}