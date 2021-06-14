using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Humbee.Api.Samples.Models;

namespace Humbee.Api.Samples
{
    public class FileUploadDownloadDeleteSample
    {
        public static async Task UploadDownloadAndDeleteFileAsync(HttpClient httpClient)
        {
            // Upload File
            await using var fileStream = new FileStream(".\\Data\\SimplePdf.pdf", FileMode.Open);

            var formContent = new MultipartFormDataContent
            {
                {new StreamContent(fileStream), "file", "SimplePdf.pdf"}
            };

            var response = await httpClient.PostAsync("/api/documents", formContent);
            response.EnsureSuccessStatusCode();
            var creationResult = await response.Content.ReadFromJsonAsync<CreationResultModel>();
            var documentId = creationResult?.Id;

            Console.WriteLine($"Document created with id = {documentId}");


            // Download file
            response = await httpClient.GetAsync($"/api/documents/{documentId}/file");
            response.EnsureSuccessStatusCode();
            var fileData = await response.Content.ReadAsByteArrayAsync();
            
            Console.WriteLine($"Downloaded file data has got {fileData.Length} bytes.");


            // Delete file
            response = await httpClient.DeleteAsync("/api/documents/"+ documentId);
            response.EnsureSuccessStatusCode();
            Console.WriteLine($"Document {documentId} has been deleted.");
        }
    }
}