using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Receiver
{
    class Program
    {
        private static string ConnectionString = "";
        private static string QueueName = "";

        static void Main(string[] args)
        {
            Console.WriteLine(@"/!\/!\/!\/!\ RECEIVER /!\/!\/!\/!\");
            while (true)
            {
                var line = Console.ReadLine();
                if (line == "stop")
                    break;
                if (line == "read")
                {
                    string value = ReceiveMessageAsync().Result;
                    Console.WriteLine($"Message Received from queue '{QueueName}' : '{value}'");
                }
            }
        }

        static CloudQueue GetQueue()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            return queueClient.GetQueueReference(QueueName);
        }

        static async Task<string> ReceiveMessageAsync()
        {
            CloudQueue queue = GetQueue();
            bool exists = await queue.ExistsAsync();
            if (exists)
            {
                CloudQueueMessage retrievedMessage = await queue.GetMessageAsync();
                if (retrievedMessage != null)
                {
                    string newMessage = retrievedMessage.AsString;
                    await queue.DeleteMessageAsync(retrievedMessage);
                    return newMessage;
                }
            }

            return "<queue empty or not created>";
        }
    }
}
