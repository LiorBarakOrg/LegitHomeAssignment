using System.Text.Json.Serialization;

namespace LegitHomeTask.Models
{
    public abstract class GitHubEvent
    {
        [JsonPropertyName("action")]
        public string Action { get; set; } = "";

        public abstract bool HandleEvent();
    }
}