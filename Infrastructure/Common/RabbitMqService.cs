using Application.Common.Interfaces;

using Microsoft.Extensions.Options;

using RabbitMQ.Client;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly RabbitMqConfiguration _configuration;
        public RabbitMqService(IOptions<RabbitMqConfiguration> options)
        {
            _configuration = options.Value;
        }
        public IConnection CreateChannel()
        {
            ConnectionFactory connection = new ConnectionFactory()
            {
                HostName = !string.IsNullOrEmpty(_configuration.HostName) ? _configuration.HostName : "localhost",
                Password = !string.IsNullOrEmpty(_configuration.Password) ? _configuration.Password : "guest",
                UserName = !string.IsNullOrEmpty(_configuration.Username) ? _configuration.Username : "guest"
            };
            connection.DispatchConsumersAsync = true;
            var channel = connection.CreateConnection();
            return channel;
        }
    }
}
