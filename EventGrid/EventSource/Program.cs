using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using System;
using System.Collections.Generic;

namespace EventSource
{
    class Program
    {
        static string topicKey = "";
        static string topicEndpoint = "";
        static string topicHostname = new Uri(topicEndpoint).Host;

        static void Main(string[] args)
        {
            TopicCredentials credentials = new TopicCredentials(topicKey);
            EventGridClient client = new EventGridClient(credentials);

            // An EventGridClient uses an HTTPClient
            //var httpClient = client.HttpClient;

            client.PublishEventsAsync(topicHostname, GetEventList()).GetAwaiter().GetResult();
            Console.Write("Published events to Event Grid.");
        }

        private static IList<EventGridEvent> GetEventList()
        {
            List<EventGridEvent> eventsList = new List<EventGridEvent>();
            for (int i = 0; i < 10; i++)
            {
                // https://docs.microsoft.com/en-us/azure/event-grid/event-schema
                // { "id": "340ef7cc-1dcd-4645-81b7-0d8f665922c7", "topic": "/subscriptions/d1fc3aa5-3154-4ece-84e6-85e2eb598016/resourceGroups/rg-avanmatest-poc/providers/Microsoft.EventGrid/topics/mytopic", "subject": "Door1", "data": { "From": "Antoine", "To": "Workshop", "Content": "9 : Hope it will be helpful" }, "eventType": "eventTypeSms", "eventTime": "2020-09-20T12:26:04.1736534Z", "metadataVersion": "1", "dataVersion": "2.0" }
                eventsList.Add(new EventGridEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    EventType = "eventTypeSms", // One of the registered event types for this event source.
                    Data = new EventSourceSmsContentEntity
                    {
                        From = "Antoine",
                        To = "Workshop",
                        Content = $"{i} : Hope it will be helpful"
                    },
                    EventTime = DateTime.Now,
                    Subject = "Door1", // Publisher-defined path to the event subject.
                    DataVersion = "2.0",
                });
            }
            return eventsList;
        }
    }
}
