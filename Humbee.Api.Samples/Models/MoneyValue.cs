using System.Text.Json.Serialization;

namespace Humbee.Api.Samples.Models
{
    public class MoneyValue
    {
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        [JsonPropertyName("currency")]
        public Currency Currency { get; set; }
    }
}