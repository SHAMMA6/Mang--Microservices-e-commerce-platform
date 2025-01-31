using RabbitMQ.Client;

namespace Mang.Services.AuthAPI.RabbitMQSender
{
    public class RabbitMQAuthMessageSender : IRabbitMQAuthMessageSender
    {
        private readonly string _hostName;
        private readonly string _userName;
        private readonly string _password;
        private IConnection _connection;

        public RabbitMQAuthMessageSender()
        {
            _hostName = "localhost";
            _userName = "guest";
            _password = "guest";
        }
        public async void SendMessage(object message, string queueName)
        {
            if (ConnectionExists())
            {
                using var channel = await _connection.CreateChannelAsync();
                await channel.QueueDeclareAsync(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var json = System.Text.Json.JsonSerializer.Serialize(message);
                var body = System.Text.Encoding.UTF8.GetBytes(json);

                await channel.BasicPublishAsync(exchange: "",
                                     routingKey: queueName,
                                     body: body);
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
