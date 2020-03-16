using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Sender
{
    class Program
    {
        private static string ConnectionString = "";
        private static string QueueName = "";

        static void Main(string[] args)
        {
            Console.WriteLine(@"/!\/!\/!\/!\ SENDER /!\/!\/!\/!\");
            while (true)
            {
                var line = Console.ReadLine();
                if (line == "stop")
                {
                    break;
                }
                else
                {
                    SendMessageAsync(line).Wait();
                    Console.WriteLine($"Message Sent to queue '{QueueName}' : '{line}'");
                }
            }
        }

        static async Task SendMessageAsync(string newMessage)
        {
            CloudQueue queue = GetQueue();
            bool createdQueue = await queue.CreateIfNotExistsAsync();
            if (createdQueue)
            {
                Console.WriteLine($"The queue '{QueueName}' was created.");
            }

            CloudQueueMessage message = new CloudQueueMessage(newMessage);
            await queue.AddMessageAsync(message);
        }

        static CloudQueue GetQueue()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            return queueClient.GetQueueReference(QueueName);
        }
    }
}
