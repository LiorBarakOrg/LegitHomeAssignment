using System.Text.Json.Serialization;

namespace LegitHomeTask.Models
{
    public class CreateTeamEvent : GitHubEvent
    {
        [JsonPropertyName("team")]
        public Team Team { get; set; } = new Team();

        // Detect suspicious team creation based on the name starting with "hacker"
        public override bool HandleEvent()
        {
            // Check if the action is "created" and if the team name contains "hacker"
            if (Action.Equals("created", StringComparison.OrdinalIgnoreCase) &&
                Team.Name.Contains("hacker", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Suspicious Team Created: {Team.Name}");
                return true;
            }
            else
            {
                // Console.WriteLine("Normal Team Created");
                return false;
            }
        }
    }

    public class Team
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
    }
}
