using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace Receiver
{
    class Program
    {
        private static string ServiceBusConnectionString = "";
        private static string TopicName = "";
        private static string SubscriptionName = "";

        static ISubscriptionClient subscriptionClient;

        static void Main(string[] args)
        {
            Console.WriteLine(@"/!\/!\/!\/!\ RECEIVER /!\/!\/!\/!\");
            subscriptionClient = new SubscriptionClient(ServiceBusConnectionString, TopicName, SubscriptionName);

            RegisterMessageHandler();

            Console.WriteLine(@"/!\ Press key when you want to close the connection to the subscription /!\");
            Console.Read();

            subscriptionClient.CloseAsync().Wait();
        }

        static void RegisterMessageHandler()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false,

            };
            subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var messageBody = Encoding.UTF8.GetString(message.Body);
            if (messageBody.ToLower().Contains("exception"))
                throw new Exception("The exception message");
            Console.WriteLine($"Message Received from Subscription '{SubscriptionName}' : '{messageBody}'");
            await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken); // Because autocomplete is false
        }

        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine($"EXCEPTION : Endpoint: {context.Endpoint} - Entity Path: {context.EntityPath}");
            return Task.CompletedTask;
        }
    }
}
