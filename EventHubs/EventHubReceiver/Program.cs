using Microsoft.Azure.EventHubs;
using Sender;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace EventHubReceiver
{
    class Program
    {
        private const string EventHubNamespaceConnectionString = "";
        private const string EventHubName = "";
        private const string ConsumerGroupName = "";
        private static PartitionReceiver _receiver;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(EventHubNamespaceConnectionString)
            {
                EntityPath = EventHubName
            };

            var client = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());

            var infos = client.GetRuntimeInformationAsync().GetAwaiter().GetResult();

            // Here we have a simple receiver. We must know the partition key & the EventPosition.
            // Determine the partition key and the event position isn't a trivial thing
            _receiver = client.CreateReceiver(ConsumerGroupName, "0", EventPosition.FromStart());
            // We can receive events using ReceiveAsync Or we can have a receive handler
            Receive(100).GetAwaiter().GetResult();
            _receiver.CloseAsync();
        }

        static async Task Receive(int number)
        {
            var sms = await _receiver.ReceiveAsync(number);
            foreach (EventData s in sms)
            {
                Console.WriteLine($"Event Read from ReceiveAsync: { FromByteArray<SmsContent>(s.Body.ToArray()) } - Partition key : {s.SystemProperties.PartitionKey}");
            }
        }

        public static T FromByteArray<T>(byte[] data)
        {
            if (data == null)
                return default(T);
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }
    }
}
