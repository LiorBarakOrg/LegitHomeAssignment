using System.Text.Json.Serialization;
using System;

namespace LegitHomeTask.Models
{
    public class RepoCreateDeleteEvent : GitHubEvent
    {
        [JsonPropertyName("repository")]
        public Repository Repository { get; set; } = new Repository();

        public override bool HandleEvent()
        {
            DateTime currentTime = DateTime.UtcNow;
            // Check if the action is "deleted" and if the repo was deleted before 10 min had passed before it was created
            if (Action.Equals("deleted", StringComparison.OrdinalIgnoreCase)
                &&
                (Repository.CreatedAt > currentTime.AddMinutes(-10)))
            {
                Console.WriteLine($"Suspicious Repo Deletion Detected: {Repository.Name} deleted within the last 10 minutes");
                return true;
            }
            else
            {
                // Console.WriteLine("Normal Repo Deletion Detected");
                return false;
            }
        }
    }

    public class Repository
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }
    }
}