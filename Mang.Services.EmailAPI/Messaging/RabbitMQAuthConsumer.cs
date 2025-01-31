using Mang.Services.EmailAPI.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace Mang.Services.EmailAPI.Messaging
{
    public class RabbitMQAuthConsumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly EmailService _emailService;
        private IConnection _connection;
        private IChannel _channel;

        public RabbitMQAuthConsumer(IConfiguration configuration, EmailService emailService)
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

            string queueName = _configuration.GetValue<string>("TopicAndQueueNames:RegisterUserQueue");
            await _channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var email = JsonConvert.DeserializeObject<string>(content);

                await HandleMessageAsync(email);
                await _channel.BasicAckAsync(ea.DeliveryTag, false);
            };

            string queueName = _configuration.GetValue<string>("TopicAndQueueNames:RegisterUserQueue");
            _channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        private async Task HandleMessageAsync(string email)
        {
            await _emailService.RegisterUserEmail(email);
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
