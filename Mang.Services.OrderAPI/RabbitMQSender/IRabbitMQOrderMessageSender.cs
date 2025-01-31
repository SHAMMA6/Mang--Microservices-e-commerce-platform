namespace Mang.Services.OrderAPI.RabbitMQSender
{

    public interface IRabbmitMQOrderMessageSender
    {
        void SendMessage(Object message, string exchangeName);
    }
}

