using Microsoft.Azure.EventHubs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Sender
{
    class Program
    {
        private const string EventHubNamespaceConnectionString = "";
        private const string EventHubName = "";

        static void Main(string[] args)
        {
            // Creates an EventHubsConnectionStringBuilder object from a the connection string, and sets the EntityPath.
            // Typically the connection string should have the Entity Path in it, but for the sake of this simple scenario
            // we are using the connection string from the namespace.
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(EventHubNamespaceConnectionString)
            {
                EntityPath = EventHubName
            };

            var client = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
            while(Console.ReadLine() != "q")
            {
                var batch = GetDataList();
                // RoundRobin over the partitions
                client.SendAsync(batch).GetAwaiter().GetResult();

                // you can also send with a particular partition key. Only if needed !!! PARTITION KEY != PARTITION NAME
                Console.WriteLine("Partition key name ?");
                var pKey = Console.ReadLine();
                client.SendAsync(batch, pKey).GetAwaiter().GetResult();
            }
            client.CloseAsync().GetAwaiter().GetResult();
        }

        private static List<EventData> GetDataList()
        {
            var myData = new List<EventData>();
            for(int i=0; i< 10; i++)
            {
                SmsContent message = new SmsContent
                {
                    From ="Antoine",
                    To="Workshop",
                    Content=$"Message {i} : Hope it will be useful"
                };
                myData.Add(new EventData(ObjectToByteArray(message)));
            }

            return myData;
        }

        private static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}
