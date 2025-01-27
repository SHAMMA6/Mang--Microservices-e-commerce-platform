using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mang.MessageBusService
{
    public class MessageBus : IMessageBus
    {
        private readonly string _connectionString = "Key From Azure";

        public async Task PublishMessageAsync(object message, string topicQueueName)
        {
            // Serialize the message into JSON
            var jsonMessage = JsonConvert.SerializeObject(message);

            // Create a new ServiceBus client
            await using (var client = new ServiceBusClient(_connectionString))
            {
                // Create a sender for the specified topic or queue
                ServiceBusSender sender = client.CreateSender(topicQueueName);

                // Create the ServiceBus message
                ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
                {
                    CorrelationId = Guid.NewGuid().ToString()
                };

                // Send the message
                await sender.SendMessageAsync(finalMessage);
            }
        }

    }
}
