using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mang.MessageBusService
{
    public interface IMessageBus
    {
        Task PublishMessageAsync(object message,string queueName );
    }
}
