using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace Sender
{
    class Program
    {
        private static string ServiceBusConnectionString = "";
        private static string TopicName = "";

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
                    Console.WriteLine($"Message Sent to topic '{TopicName}' : '{line}'");
                }
            }
        }

        private static async Task SendMessageAsync(string messageBody)
        {
            ITopicClient topicClient = new TopicClient(ServiceBusConnectionString, TopicName);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));
            await topicClient.SendAsync(message);
            await topicClient.CloseAsync();
        }
    }
}
