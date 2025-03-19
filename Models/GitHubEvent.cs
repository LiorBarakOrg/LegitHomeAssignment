using System.Text.Json.Serialization;

namespace LegitHomeTask.Models
{
    public abstract class GitHubEvent
    {
        [JsonPropertyName("action")]
        public string Action { get; set; } = "";

        // [JsonPropertyName("Organization")]
        // public Organization Organization { get; set; } = new Organization();
        // [JsonPropertyName("sender")]
        // public Sender Sender { get; set; } = new Sender();

        public abstract bool HandleEvent();
    }

    // public class Organization
    // {
    //     [JsonPropertyName("login")]
    //     public string Login { get; set; } = "";

    //     [JsonPropertyName("id")]
    //     public int Id { get; set; }

    //     [JsonPropertyName("node_id")]
    //     public string NodeId { get; set; } = "";

    //     [JsonPropertyName("url")]
    //     public string Url { get; set; } = "";

    //     [JsonPropertyName("repos_url")]
    //     public string ReposUrl { get; set; } = "";

    //     [JsonPropertyName("events_url")]
    //     public string EventsUrl { get; set; } = "";

    //     [JsonPropertyName("hooks_url")]
    //     public string HooksUrl { get; set; } = "";

    //     [JsonPropertyName("issues_url")]
    //     public string IssuesUrl { get; set; } = "";

    //     [JsonPropertyName("members_url")]
    //     public string MembersUrl { get; set; } = "";

    //     [JsonPropertyName("public_members_url")]
    //     public string PublicMembersUrl { get; set; } = "";

    //     [JsonPropertyName("avatar_url")]
    //     public string AvatarUrl { get; set; } = "";

    //     [JsonPropertyName("description")]
    //     public string Description { get; set; } = "";
    // }

    // public class Sender
    // {
    //     [JsonPropertyName("login")]
    //     public string Login { get; set; } = "";

    //     [JsonPropertyName("id")]
    //     public int Id { get; set; }

    //     [JsonPropertyName("node_id")]
    //     public string NodeId { get; set; } = "";

    //     [JsonPropertyName("avatar_url")]
    //     public string AvatarUrl { get; set; } = "";

    //     [JsonPropertyName("gravatar_id")]
    //     public string GravatarId { get; set; } = "";

    //     [JsonPropertyName("url")]
    //     public string Url { get; set; } = "";

    //     [JsonPropertyName("html_url")]
    //     public string HtmlUrl { get; set; } = "";

    //     [JsonPropertyName("followers_url")]
    //     public string FollowersUrl { get; set; } = "";

    //     [JsonPropertyName("following_url")]
    //     public string FollowingUrl { get; set; } = "";

    //     [JsonPropertyName("gists_url")]
    //     public string GistsUrl { get; set; } = "";

    //     [JsonPropertyName("starred_url")]
    //     public string StarredUrl { get; set; } = "";

    //     [JsonPropertyName("subscriptions_url")]
    //     public string SubscriptionsUrl { get; set; } = "";

    //     [JsonPropertyName("organizations_url")]
    //     public string OrganizationsUrl { get; set; } = "";

    //     [JsonPropertyName("repos_url")]
    //     public string ReposUrl { get; set; } = "";

    //     [JsonPropertyName("events_url")]
    //     public string EventsUrl { get; set; } = "";

    //     [JsonPropertyName("received_events_url")]
    //     public string ReceivedEventsUrl { get; set; } = "";

    //     [JsonPropertyName("type")]
    //     public string Type { get; set; } = "";

    //     [JsonPropertyName("user_view_type")]
    //     public string UserViewType { get; set; } = "";

    //     [JsonPropertyName("site_admin")]
    //     public bool SiteAdmin { get; set; }
    // }
}