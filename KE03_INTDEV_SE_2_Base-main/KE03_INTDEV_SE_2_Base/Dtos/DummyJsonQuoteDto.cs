using System.Text.Json.Serialization;

namespace KE03_INTDEV_SE_2_Base.Dtos
{
    public class DummyJsonQuoteDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("quote")]
        public string Quote { get; set; } = string.Empty;

        [JsonPropertyName("author")]
        public string Author { get; set; } = string.Empty;
    }
}