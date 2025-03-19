namespace LegitHomeTask.Models
{
    public class PushCodeEvent : GitHubEvent
    {
        public string RepositoryName { get; set; } = "";
        public string CommitMessage { get; set; } = "";

        public override bool HandleEvent()
        {
            // if (pushCodeEvent.Timestamp.Hour >= 14 && pushCodeEvent.Timestamp.Hour <= 16)
            // {
            //     Console.WriteLine($"Detected suspicious behavior: 'Pushing code event between 14:00-16:00'...{pushCodeEvent.CommitMessage}");
            // }
            Console.WriteLine($"Push Code Event: {RepositoryName} - {CommitMessage}");
            return false;
        }
    }
}