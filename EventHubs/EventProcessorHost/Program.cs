using System;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;

namespace EventProcessorHost
{
    class Program
    {
        private const string EventHubNamespaceConnectionString = "";
        private const string EventHubName = "";
        private const string StorageContainerName = "";
        private const string StorageAccountName = "";
        private const string StorageAccountKey = "";
        private static readonly string StorageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", StorageAccountName, StorageAccountKey);

        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Registering EventProcessor...");

            // Ownership of a partition to an EPH instance (or a consumer) is tracked through the Azure Storage account that is provided for tracking.
            // https://docs.microsoft.com/en-us/azure/event-hubs/event-hubs-event-processor-host 
            var eventProcessorHost = new Microsoft.Azure.EventHubs.Processor.EventProcessorHost(
                EventHubName,
                PartitionReceiver.DefaultConsumerGroupName,
                EventHubNamespaceConnectionString,
                StorageConnectionString,
                StorageContainerName);

            // Registers the Event Processor Host and starts receiving messages
            await eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>();

            Console.WriteLine("Receiving. Press enter key to stop worker.");
            Console.ReadLine();

            // Disposes of the Event Processor Host
            await eventProcessorHost.UnregisterEventProcessorAsync();
        }
    }
}
