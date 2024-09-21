using RabbitMQ.Client;

namespace Application.Common.Interfaces
{
    public interface IRabbitMqService
    {
        IConnection CreateChannel();
    }
}
