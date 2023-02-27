using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Humbee.Api.Samples.Models
{
    public class NewCaseModel
    {
        [JsonPropertyName("caption")]
        public string Caption { get; set; }
    
        [JsonPropertyName("type")]
        public CaseTypeApiModel Type { get; set; }

        /// <summary>
        /// Pairs of property id and value to be assigned to the new case.
        /// </summary>
        public Dictionary<string, object> Properties { get; set; }

        /// <summary>
        /// Given the namespace of a foreign key pool. E.g. the name of your client application. If not specified, then 'humbee' is the default value.
        /// </summary>
        public string ExternalReferenceNamespace { get; set; }
        /// <summary>
        /// The name of a specific pool the foreign key is part of. E.g. the id of the system where the key belongs to. If not specified, then 'restapi' is the default value.
        /// </summary>
        public string ExternalReferencePoolId { get; set; }
        /// <summary>
        /// The value of the foreign key. The foreign key should be unique for all keys belonging to the same namespace and id pool.
        /// </summary>
        public string ExternalReferenceId { get; set; }
    }
}