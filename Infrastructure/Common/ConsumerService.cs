using Application.Common.Interfaces;

using Domain;

using Microsoft.Extensions.DependencyInjection;

using OfficeOpenXml;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Text;

namespace Infrastructure.Common
{
    public class ConsumerService : IConsumerService, IDisposable
    {
        private readonly IModel _model;
        private readonly IConnection _connection;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ConsumerService(IRabbitMqService rabbitMqService, IServiceScopeFactory serviceScopeFactory)
        {
            _connection = rabbitMqService.CreateChannel();
            _model = _connection.CreateModel();
            _model.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);
            _model.ExchangeDeclare("ProcessFile", ExchangeType.Fanout, durable: true, autoDelete: false);
            _model.QueueBind(_queueName, "ProcessFile", string.Empty);
            _serviceScopeFactory = serviceScopeFactory;
        }
        const string _queueName = "ProcessFile";
        public async Task ReadMessgaes()
        {
            var consumer = new AsyncEventingBasicConsumer(_model);
            consumer.Received += async (ch, ea) =>
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    //var data = Convert.FromBase64String(message);

                    // پردازش و درج رکوردها
                    using var stream = new MemoryStream(body);
                    using var package = new ExcelPackage(stream);
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var mydata = new MyData
                        {
                            Data = worksheet.Cells[row, 2].Text
                        };
                        await _dbContext.MyData.AddAsync(mydata);
                    }

                    await _dbContext.SaveChangesAsync();

                    await Task.CompletedTask;
                    _model.BasicAck(ea.DeliveryTag, false);
                }
            };
            _model.BasicConsume(_queueName, false, consumer);
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            if (_model.IsOpen)
                _model.Close();
            if (_connection.IsOpen)
                _connection.Close();
        }
    }
}
