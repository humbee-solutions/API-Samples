using System.Text.Json.Serialization;

namespace Humbee.Api.Samples.Models
{
    public class CaseTypeApiModel
    {
        /// <summary>
        /// Unique identifier for the case type
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Descriptive name of the case type
        /// </summary>
        [JsonPropertyName("caption")]
        public string Caption { get; set; }

        /// <summary>
        /// Optional shorthand identifier for the case type
        /// </summary>
        [JsonPropertyName("reference")]
        public string Reference { get; set; }
    }
}