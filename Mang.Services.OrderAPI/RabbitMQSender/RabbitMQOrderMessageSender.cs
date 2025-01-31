using RabbitMQ.Client;

namespace Mang.Services.OrderAPI.RabbitMQSender
{
    public class RabbitMQOrderMessageSender : IRabbitMQOrderMessageSender
    {
        private readonly string _hostName;
        private readonly string _userName;
        private readonly string _password;
        private IConnection _connection;
        private const string OrderCreated_RewardsUpdateQueue = "RewardsUpdateQueue";
        private const string OrderCreated_EmailUpdateQueue = "EmailUpdateQueue";

        public RabbitMQOrderMessageSender()
        {
            _hostName = "localhost";
            _userName = "guest";
            _password = "guest";
        }
        public async void SendMessage(object message, string exchangName)
        {
            if (ConnectionExists())
            {
                using var channel = await _connection.CreateChannelAsync();
                await channel.ExchangeDeclareAsync(exchangName,
                                     ExchangeType.Direct,
                                     durable: false,
                                     false,
                                     null);
                await channel.QueueDeclareAsync(OrderCreated_RewardsUpdateQueue, false, false, false, null);
                await channel.QueueDeclareAsync(OrderCreated_EmailUpdateQueue, false, false, false, null);

                await channel.QueueBindAsync(OrderCreated_RewardsUpdateQueue, exchangName, routingKey: "RewardsUpdate");
                await channel.QueueBindAsync(OrderCreated_EmailUpdateQueue, exchangName, routingKey: "EmailUpdate"); 

                var json = System.Text.Json.JsonSerializer.Serialize(message);
                var body = System.Text.Encoding.UTF8.GetBytes(json);

                await channel.BasicPublishAsync(exchangName, routingKey: "RewardsUpdate", false, body: body);
                await channel.BasicPublishAsync(exchangName, routingKey: "EmailUpdate", false, body: body);
            }
        }

        private async Task CreateConnectionAsync()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _hostName,
                    UserName = _userName,
                    Password = _password
                };

                _connection = await factory.CreateConnectionAsync();
            }
            catch (System.Exception)
            {
                
            }
        }
        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }
            CreateConnectionAsync().GetAwaiter();
            return true;
        }
    }
}
