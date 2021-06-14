using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Humbee.Api.Samples.Models;

namespace Humbee.Api.Samples
{
    public class FileUploadWithExternalIdSample
    {
        public static async Task UploadDownloadAndDeleteFileAsync(HttpClient httpClient)
        {
            const string foreignKey = "abcdef-1234-ghi-567";
            const string foreignKeyPool = "TestSystem 47b";
            const string foreignNamespace = "Foobar-Inc-MyCoolApp";

            // Upload File
            await using var fileStream = new FileStream(".\\Data\\SimplePdf.pdf", FileMode.Open);

            var formContent = new MultipartFormDataContent
            {
                {new StringContent(foreignNamespace), "ExternalReferenceNamespace"},
                {new StringContent(foreignKeyPool), "ExternalReferencePoolId"},
                {new StringContent(foreignKey), "ExternalReferenceId"},
                {new StreamContent(fileStream), "file", "SimplePdf.pdf"}
            };

            var response = await httpClient.PostAsync("/api/documents", formContent);
            response.EnsureSuccessStatusCode();
            var creationResult = await response.Content.ReadFromJsonAsync<CreationResultModel>();
            var humbeeDocumentId = creationResult?.Id;

            Console.WriteLine($"Document created with id = {humbeeDocumentId}");

            // Lookup Document by foreign key
            response = await httpClient.GetAsync($"/api/documents/namespace/{foreignNamespace}/pool/{foreignKeyPool}/key/{foreignKey}");
            response.EnsureSuccessStatusCode();

            // Download file by foreign key
            response = await httpClient.GetAsync($"/api/documents/namespace/{foreignNamespace}/pool/{foreignKeyPool}/key/{foreignKey}/file");
            response.EnsureSuccessStatusCode();
            var fileData = await response.Content.ReadAsByteArrayAsync();

            Console.WriteLine($"Downloaded file data has got {fileData.Length} bytes.");


            // Delete file
            response = await httpClient.DeleteAsync("/api/documents/" + humbeeDocumentId);
            response.EnsureSuccessStatusCode();
            Console.WriteLine($"Document {humbeeDocumentId} has been deleted.");
        }
    }
}