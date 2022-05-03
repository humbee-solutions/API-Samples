using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Humbee.Api.Samples.Models;

namespace Humbee.Api.Samples
{
    public class DocumentFulltextSearchSample
    {
        public static async Task UploadDownloadAndSearchForCaptionAsync(HttpClient httpClient)
        {
            // Upload File
            await using var fileStream = new FileStream(".\\Data\\SimplePdf.pdf", FileMode.Open);

            var formContent = new MultipartFormDataContent
            {
                {new StreamContent(fileStream), "file", "Simple Pdf 123456.pdf"}
            };

            var response = await httpClient.PostAsync("/api/documents", formContent);
            response.EnsureSuccessStatusCode();
            var creationResult = await response.Content.ReadFromJsonAsync<CreationResultModel>();
            var humbeeDocumentId = creationResult?.Id;

            Console.WriteLine($"Document created with id = {humbeeDocumentId}");

            // Search for document using fulltext and ordering by created date (descending)

            var fulltext = "simple";
            var orderBy = "created desc";

            response = await httpClient.GetAsync($"/api/documents?search={Uri.EscapeDataString(fulltext)}&$orderBy={Uri.EscapeDataString(orderBy)}");
            response.EnsureSuccessStatusCode();
            var searchResult = await response.Content.ReadFromJsonAsync<DocumentsPagedListResult>();
            Console.WriteLine(searchResult?.Documents?.FirstOrDefault()?.Id);

            // Delete file - cleanup sample document
            response = await httpClient.DeleteAsync("/api/documents/" + humbeeDocumentId);
            response.EnsureSuccessStatusCode();
            Console.WriteLine($"Document {humbeeDocumentId} has been deleted.");
        }
    }

    public class DocumentsPagedListResult
    {
        [JsonPropertyName("value")]
        public IEnumerable<DocumentModel> Documents { get; set; }
    }

    public class DocumentModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("caption")]
        public string Caption { get; set; }
    }
}