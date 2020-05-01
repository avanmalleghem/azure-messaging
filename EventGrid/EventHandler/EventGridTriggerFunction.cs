// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using System.IO;
using Newtonsoft.Json;
using EventSource;
using Newtonsoft.Json.Linq;

namespace EventHandler
{
    public static class EventGridTriggerFunction
    {
        [FunctionName("EventGridTriggerFunction")]
        public static void Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {
            log.LogInformation(JsonConvert.SerializeObject(eventGridEvent, Formatting.Indented));
            log.LogInformation($"Data is EventSourceSmsContentEntity : {eventGridEvent.Data is EventSourceSmsContentEntity}");
            log.LogInformation($"Data is SmsContent : {eventGridEvent.Data is SmsContent}");

            // Cast event data to a Storage Blob Created Event
            SmsContent data = ((JObject)eventGridEvent.Data).ToObject<SmsContent>();
            log.LogInformation(data.ToString());
        }
    }
}
