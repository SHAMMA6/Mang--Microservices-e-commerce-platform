using Mang.Services.EmailAPI.Message;
using Mang.Services.EmailAPI.Models.Dto;
using Mang.Services.EmailAPI.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace Mang.Services.EmailAPI.Messaging
{
    public class RabbitMQOrderConsumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly EmailService _emailService;
        private IConnection _connection;
        private IChannel _channel;
        private const string OrderCreated_EmailUpdateQueue = "EmailUpdateQueue";
        private  string ExchangName = "";
        string queueName = "";

        public RabbitMQOrderConsumer(IConfiguration configuration, EmailService emailService)
        {
            _configuration = configuration;
            _emailService = emailService;

            InitializeRabbitMQAsync().GetAwaiter().GetResult();
        }

        private async Task InitializeRabbitMQAsync()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            string queueName = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreatedTopic",ExchangeType.Direct);
            await _channel.QueueDeclareAsync(OrderCreated_EmailUpdateQueue, false, false, false, null);
            await _channel.QueueBindAsync(OrderCreated_EmailUpdateQueue, ExchangName, routingKey: "EmailUpdate");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                RewardsMessage rewardMessage = JsonConvert.DeserializeObject<RewardsMessage>(content);

                await HandleMessageAsync(rewardMessage);
                await _channel.BasicAckAsync(ea.DeliveryTag, false);
            };

            string queueName = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreatedTopic");
            _channel.BasicConsumeAsync(queue: OrderCreated_EmailUpdateQueue, autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        private async Task HandleMessageAsync(RewardsMessage rewardMessage)
        {
            await _emailService.LogOrderPlaced(rewardMessage);
        }

        public override void Dispose()
        {
            _channel?.CloseAsync();
            _channel?.Dispose();
            _connection?.CloseAsync();
            _connection?.Dispose();
            base.Dispose();
        }
    }
}
