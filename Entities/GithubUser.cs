using System;
using System.Text.Json.Serialization;

namespace Entities
{
    public class GithubUser
    {
        [JsonPropertyName("login")]
        public string Login { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("node_id")]
        public string NodeId { get; set; }

        [JsonPropertyName("avatar_url")]
        public Uri AvatarUrl { get; set; }

        [JsonPropertyName("gravatar_id")]
        public string GravatarId { get; set; }

        [JsonPropertyName("url")]
        public Uri Url { get; set; }

        [JsonPropertyName("html_url")]
        public Uri HtmlUrl { get; set; }

        [JsonPropertyName("followers_url")]
        public Uri FollowersUrl { get; set; }

        [JsonPropertyName("following_url")]
        public string FollowingUrl { get; set; }

        [JsonPropertyName("gists_url")]
        public string GistsUrl { get; set; }

        [JsonPropertyName("starred_url")]
        public string StarredUrl { get; set; }

        [JsonPropertyName("subscriptions_url")]
        public Uri SubscriptionsUrl { get; set; }

        [JsonPropertyName("organizations_url")]
        public Uri OrganizationsUrl { get; set; }

        [JsonPropertyName("repos_url")]
        public Uri ReposUrl { get; set; }

        [JsonPropertyName("events_url")]
        public string EventsUrl { get; set; }

        [JsonPropertyName("received_events_url")]
        public Uri ReceivedEventsUrl { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("site_admin")]
        public bool SiteAdmin { get; set; }

        [JsonPropertyName("name")]
        public object Name { get; set; }

        [JsonPropertyName("company")]
        public object Company { get; set; }

        [JsonPropertyName("blog")]
        public string Blog { get; set; }

        [JsonPropertyName("location")]
        public object Location { get; set; }

        [JsonPropertyName("email")]
        public object Email { get; set; }

        [JsonPropertyName("hireable")]
        public object Hireable { get; set; }

        [JsonPropertyName("bio")]
        public object Bio { get; set; }

        [JsonPropertyName("public_repos")]
        public long PublicRepos { get; set; }

        [JsonPropertyName("public_gists")]
        public long PublicGists { get; set; }

        [JsonPropertyName("followers")]
        public long Followers { get; set; }

        [JsonPropertyName("following")]
        public long Following { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}