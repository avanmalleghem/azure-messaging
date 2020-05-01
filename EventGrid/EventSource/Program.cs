﻿using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using System;
using System.Collections.Generic;

namespace EventSource
{
    class Program
    {
        static string topicKey = "0zm+QaGlSpcCbE3Awq/ZrYl7kpo/o39junVUDXncWfo=";
        static string topicEndpoint = "https://firsttopic1234.francecentral-1.eventgrid.azure.net/api/events";
        static string topicHostname = new Uri(topicEndpoint).Host;
        static string eventType = "eventTypeSms";

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
                eventsList.Add(new EventGridEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    EventType = eventType,
                    Data = new EventSourceSmsContentEntity
                    {
                        From = "Antoine",
                        To = "Workshop",
                        Content = $"{i} : Hope it will be helpful"
                    },
                    EventTime = DateTime.Now,
                    Subject = "Door1",
                    DataVersion = "2.0"
                });
            }
            return eventsList;
        }
    }
}
