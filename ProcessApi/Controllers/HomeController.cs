using Microsoft.AspNetCore.Mvc;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Text;

namespace ProcessApi.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "index")]
    public async Task<IActionResult> Get()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: "ProcessFile", durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += ProcessFile;

        channel.BasicConsume(queue: "ProcessFile", autoAck: true, consumer: consumer);

        return Accepted();
    }

    private void ProcessFile(object? sender, BasicDeliverEventArgs e)
    {
        var message = Encoding.UTF8.GetString(e.Body.ToArray());
    }
}
