using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.Common;
using System.Configuration;
using System.Net.Http;

using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using System.Text;
// using System.Configuration; // Removed as it's not needed

namespace Company.Function
{
    public static class GetResumeCounter
    {
        [FunctionName("GetResumeCounter")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(databaseName:"AzureResume", collectionName:"Counter", ConnectionStringSetting = "AzureResumeConnectionString", Id = "1", PartitionKey = "1")] Counter counter,
            [CosmosDB(databaseName:"AzureResume", collectionName:"Counter", ConnectionStringSetting = "AzureResumeConnectionString", Id = "1", PartitionKey = "1")] out Counter updatedcounter,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            updatedcounter = counter;
            updatedcounter.Count++;

            var jsonToReturn = JsonConvert.SerializeObject(counter);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
            };
        }
    }
}
