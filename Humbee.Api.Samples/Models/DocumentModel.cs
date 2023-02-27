using System.Text.Json.Serialization;

namespace Humbee.Api.Samples.Models
{
    public class DocumentModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("caption")]
        public string Caption { get; set; }
    }
}