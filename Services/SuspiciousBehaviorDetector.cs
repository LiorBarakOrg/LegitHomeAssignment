using LegitHomeTask.Models;
using System.Text.Json;

namespace LegitHomeTask.Services
{
    public class SuspiciousBehaviorDetector
    {
        // Process the event and invoke the corresponding HandleEvent method based on the event type.
        public void ProcessEvent(JsonElement jsonRoot, string eventType)
        {
            var githubEvent = ParseEvent(jsonRoot, eventType);
            // Call the appropriate HandleEvent method
            githubEvent.HandleEvent();
        }

        // Parse the event based on its content and infer the event type.
        public GitHubEvent ParseEvent(JsonElement jsonRoot, string eventType)
        {
            switch (eventType)
            {
                case "team":
                    return JsonSerializer.Deserialize<CreateTeamEvent>(jsonRoot)
                        ?? throw new InvalidOperationException("Failed to deserialize CreateTeamEvent");

                case "commitMessage":
                    Console.WriteLine("Commit Message Event detected!");
                    var pushCodeEvent = JsonSerializer.Deserialize<PushCodeEvent>(jsonRoot)
                        ?? throw new InvalidOperationException("Failed to deserialize PushCodeEvent");
                    Console.WriteLine($"Push Code Event detected! {pushCodeEvent}");
                    return pushCodeEvent;
                // return JsonSerializer.Deserialize<PushCodeEvent>(jsonRoot.GetRawText())
                //     ?? throw new InvalidOperationException("Failed to deserialize PushCodeEvent.");

                case "repository":
                    return JsonSerializer.Deserialize<RepoCreateDeleteEvent>(jsonRoot)
                        ?? throw new InvalidOperationException("Failed to deserialize RepoCreateDeleteEvent");

                default:
                    throw new InvalidOperationException("Unknown event type or missing necessary properties.");
            }
        }
    }
}