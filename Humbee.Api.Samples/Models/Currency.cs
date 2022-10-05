using System.Text.Json.Serialization;

namespace Humbee.Api.Samples.Models
{
    public class Currency
    {
        /// <summary>
        ///  ISO 4217 Alphabetic Code, e.G. EUR, USD
        /// </summary>
        /// 
        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}