using Humbee.Api.Samples.Models;
using System.IO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Humbee.Api.Samples
{
    public class CreateCaseSample
    {
        public static async Task CreateGeneralCaseWithCaptionAsync(HttpClient httpClient)
        {
            var jsonContent = JsonContent.Create(
                new NewCaseModel
                {
                    Caption = "A simple case"
                }
            );

            var response = await httpClient.PostAsync("/api/cases", jsonContent);
            response.EnsureSuccessStatusCode();
            var creationResult = await response.Content.ReadFromJsonAsync<CreationResultModel>();
            var humbeeCaseId = creationResult?.Id;

            Console.WriteLine($"Case created with id = {humbeeCaseId}");

            // Delete file
            response = await httpClient.DeleteAsync("/api/cases/" + humbeeCaseId);
            response.EnsureSuccessStatusCode();
            Console.WriteLine($"Case {humbeeCaseId} has been deleted.");
        }

        public static async Task CreateCaseOfSpecificTypeWithCustomPropertiesAsync(HttpClient httpClient)
        {
            // Property Id is derived from the UUID of the custom property (e.g. 0da9e6a2-b676-4f73-b3d4-3912844ffaa3) by
            // prepending the letter "p" and removing the dashes.
            // Example: The property id for uuid 0da9e6a2-b676-4f73-b3d4-3912844ffaa3 is p0da9e6a2b6764f73b3d43912844ffaa3

            const string textPropertyId = "pde38ae3f1cc2402ba9c0f5adf1bfd237";  // change to a specific text property of your tenant
            const string textPropertyValue = "test value";
            const string moneyPropertyId = "p3956331ae2b349649e7fb2e8306915d5"; // change to a specific money property of your tenant
            const string datePropertyId = "p97a5bb0c0c9a4efeaca92cce71c862a3"; // change to a specific date property of your tenant
            const string caseTypeId = "3d06860a-e4ec-4a6f-8b1c-d7e3aac2b613";  // change to a specific document type of your tenant

            var jsonContent = JsonContent.Create(
                new NewCaseModel
                {
                    Caption = "A specific case",
                    Type = new CaseTypeApiModel
                    {
                        Id = caseTypeId
                    },
                    Properties = new Dictionary<string, object>
                    {
                        { textPropertyId, textPropertyValue },
                        { datePropertyId, DateTime.UtcNow },
                        {
                            moneyPropertyId,
                            new MoneyValue
                            {
                                Amount = 45m,
                                Currency = new Currency
                                {
                                    Code = "USD"
                                }
                            }
                        }
                    }
                }
            );

            var response = await httpClient.PostAsync("/api/cases", jsonContent);
            response.EnsureSuccessStatusCode();
            var creationResult = await response.Content.ReadFromJsonAsync<CreationResultModel>();
            var humbeeCaseId = creationResult?.Id;

            Console.WriteLine($"Case created with id = {humbeeCaseId}");

            // Delete file
//            response = await httpClient.DeleteAsync("/api/cases/" + humbeeCaseId);
            response.EnsureSuccessStatusCode();
            Console.WriteLine($"Case {humbeeCaseId} has been deleted.");
        }
    }
}