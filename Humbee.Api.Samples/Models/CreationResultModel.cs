using System.Text.Json.Serialization;

namespace Humbee.Api.Samples.Models
{
    public class CreationResultModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}