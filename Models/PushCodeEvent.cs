using System.Text.Json.Serialization;
using System;

namespace LegitHomeTask.Models
{
    public class PushCodeEvent : GitHubEvent
    {
        [JsonPropertyName("commits")]
        public List<Commit> Commits { get; set; } = new List<Commit>();

        [JsonPropertyName("repository")]
        public PushRepository PushRepository { get; set; } = new PushRepository();

        public override bool HandleEvent()
        {
            // Get the latest commit (the last one in the list)
            var latestCommit = Commits.LastOrDefault();
            if (latestCommit == null)
            {
                Console.WriteLine("No commits found");
                return false;
            }

            // Parse the timestamp of the latest commit
            DateTime commitTimestamp = latestCommit.Timestamp;

            // Check if the commit timestamp is between 14:00 (2 PM) and 16:00 (4 PM)
            if (commitTimestamp.Hour >= 14 && commitTimestamp.Hour <= 16)
            {
                Console.WriteLine($"Suspicious Push Code Event Detected: {PushRepository.Name} - {latestCommit.Message}");
                return true;
            }
            else
            {
                // Console.WriteLine("The commit time is not between 14:00 and 16:00");
                return false;
            }
        }
    }

    public class Commit
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = "";

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }

    public class PushRepository
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
    }
}