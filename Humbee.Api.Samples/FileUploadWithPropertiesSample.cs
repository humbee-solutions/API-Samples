using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Humbee.Api.Samples.Models;

namespace Humbee.Api.Samples
{
    public class FileUploadWithPropertiesSample
    {
        public static async Task UploadDownloadAndDeleteFileAsync(HttpClient httpClient)
        {
            // Property Id is derived from the UUID of the custom property (e.g. 0da9e6a2-b676-4f73-b3d4-3912844ffaa3) by
            // prepending the letter "p" and removing the dashes.
            // Example: The property id for uuid 0da9e6a2-b676-4f73-b3d4-3912844ffaa3 is p0da9e6a2b6764f73b3d43912844ffaa3

            const string propertyId = "p0da9e6a2b6764f73b3d43912844ffaa3";  // change to a specific text property of your tenant
            const string propertyValue = "test value";
            const string documentTypeId = "9cdf5595-1b36-463e-93fe-a40cc8d4f1d9";  // change to a specific document type of your tenant

            // Upload File
            await using var fileStream = new FileStream(".\\Data\\SimplePdf.pdf", FileMode.Open);

            var formContent = new MultipartFormDataContent
            {
                {new StringContent(documentTypeId), "DocumentTypeId"},
                {new StringContent(propertyValue), $"Properties[{propertyId}]"},
                {new StreamContent(fileStream), "file", "SimplePdf.pdf"}
            };

            var response = await httpClient.PostAsync("/api/documents", formContent);
            response.EnsureSuccessStatusCode();
            var creationResult = await response.Content.ReadFromJsonAsync<CreationResultModel>();
            var humbeeDocumentId = creationResult?.Id;

            Console.WriteLine($"Document created with id = {humbeeDocumentId}");

            // Delete file
            response = await httpClient.DeleteAsync("/api/documents/" + humbeeDocumentId);
            response.EnsureSuccessStatusCode();
            Console.WriteLine($"Document {humbeeDocumentId} has been deleted.");
        }
    }
}