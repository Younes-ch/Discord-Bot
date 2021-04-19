using System.Text.Json.Serialization;

namespace DiscordBot
{
    public class ConfigJson
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("prefix")]
        public string Prefix { get; set; }
    }
}
