using System.Text.Json.Serialization;

namespace KE03_INTDEV_SE_2_Base.Dtos
{
    public class DummyJsonCartDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("products")]
        public List<DummyJsonCartProductDto> Products { get; set; } = new List<DummyJsonCartProductDto>();

        [JsonPropertyName("total")]
        public decimal Total { get; set; }

        [JsonPropertyName("totalProducts")]
        public int TotalProducts { get; set; }

        [JsonPropertyName("totalQuantity")]
        public int TotalQuantity { get; set; }

        [JsonPropertyName("userId")]
        public int UserId { get; set; }
    }
}
