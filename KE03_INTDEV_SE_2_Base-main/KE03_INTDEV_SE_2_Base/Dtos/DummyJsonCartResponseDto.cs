using System.Text.Json.Serialization;

namespace KE03_INTDEV_SE_2_Base.Dtos
{
    public class DummyJsonCartResponseDto
    {
        [JsonPropertyName("carts")]
        public List<DummyJsonCartDto> Carts { get; set; } = new List<DummyJsonCartDto>();
    }
}
