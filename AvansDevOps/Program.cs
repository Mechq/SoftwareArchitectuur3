using AvansDevOps.Notification;
using AvansDevOps.ProjectManagement;
using AvansDevOps.ProjectManagement.Composite;
using AvansDevOps.Report;
using AvansDevOps.SprintFinish;
using AvansDevOps.SprintFinish.Pipeline;
using AvansDevOps.Thread;
using AvansDevOps.User;
using AvansDevOps.VersionControl;
using AvansDevOps.VersionControl.Adapter;
using AvansDevOps.VersionControl.Service;

namespace AvansDevOps;
internal class Program
{

    private static void Main()
    {
        INotification emailNotifier = new EmailAdapter(new EmailService());
        INotification slackNotifier = new SlackAdapter(new SlackService());
        IVersionControl gitHubCreator = new GitHubAdapter(new GitHubService());
        IVersionControl jenkinsCreator = new JenkinsAdapter(new JenkinsService());
        IPipelineToolFactory factory = new DefaultPipelineFactory();
        var productOwner = new ProductOwner("Stef Rensma", "stef@gmail.com", "123", 31622434);
        ProjectComposite project = new ProjectComposite("Portfolio", productOwner, gitHubCreator);
        Console.WriteLine("-- Created project --\n");
        
        BacklogItemComposite backlogItemOne = new BacklogItemComposite("Diagram maken", "UML klassen Diagram tekenen.", gitHubCreator, slackNotifier);
        project.backlog.Add(backlogItemOne);
        BacklogItemComposite backlogItemTwo = new BacklogItemComposite("Github repository aanmaken",
            "Maak een GitHub repoistory voor de frontend en backend.", gitHubCreator, slackNotifier);
        project.backlog.Add(backlogItemTwo);
        Console.WriteLine("-- Created backlog items in project --\n");
        
        var start = DateTime.Now;
        var end = DateTime.Now.AddDays(7);
        SprintComposite sprintOne = new SprintComposite(
            "Week 1, Startup", start, end,
            new FeedbackSprintStrategy(slackNotifier, factory));
        SprintComposite sprintTwo = new SprintComposite(
            "Week 2, Code grind", end, DateTime.Now.AddDays(14),
            new ReleaseSprintStrategy(slackNotifier, factory));
        project.Add(sprintOne);
        project.Add(sprintTwo);
        Console.WriteLine("-- Created sprints in project--\n");
        
        sprintOne.RunPipeline();
        sprintTwo.RunPipeline();
        Console.WriteLine("-- Ran the pipeline --\n");

        
        BacklogItemComposite sprintBacklogItemOne = new BacklogItemComposite("Onderzoek naar techstack",
            "Ga op onderzoek om te kijken wat de beste techstack voor de frontend en backend is", gitHubCreator, slackNotifier);
        BacklogItemComposite sprintBacklogItemTwo =
            new BacklogItemComposite("Doe software engineering", "Voer de eerste drie stappen van het SDLC uit", gitHubCreator, slackNotifier);
        sprintOne.backlog.Add(sprintBacklogItemOne);
        sprintOne.backlog.Add(sprintBacklogItemTwo);
        
        project.backlog.MoveBacklogItem(backlogItemOne, sprintOne.backlog);
        Console.WriteLine("-- Created backlog items in sprint --\n");
        
        
        
        ActivityLeaf activityOne =
            new ActivityLeaf("Frontend studie", "Kijk naar de beste technologieen voor web applicaties");
        ActivityLeaf activityTwo =
            new ActivityLeaf("Backend studie", "Kijk naar de beste technologieen voor backend servers");
        sprintBacklogItemOne.Add(activityOne);
        sprintBacklogItemOne.Add(activityTwo);
        Console.WriteLine("-- Created activities in backlog items --\n");
        
        Discussion discussionOne = new Discussion("React vs Angular for frontend",
            new List<string> {"I think react is wayyy better then Angular", "No, Angular is so much more optimized!"}, slackNotifier);
        sprintBacklogItemOne.AddObserver(discussionOne);
        String message = "What about vue?!";
        discussionOne.AddMessage(message);
        Console.WriteLine("-- Created messages in backlog items --\n");
        
        IReportStrategy PDFreportStrategy = new PDFReport();
        IReportStrategy PNGreportStrategy = new PNGReport();
        sprintOne.GenerateReport(PDFreportStrategy, true, true);
        Console.WriteLine("-- Created report --\n");

    }
    


    
}