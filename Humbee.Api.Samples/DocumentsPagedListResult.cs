using System.Collections.Generic;
using System.Text.Json.Serialization;
using Humbee.Api.Samples.Models;

namespace Humbee.Api.Samples
{
    public class DocumentsPagedListResult
    {
        [JsonPropertyName("value")]
        public IEnumerable<DocumentModel> Documents { get; set; }
    }
}