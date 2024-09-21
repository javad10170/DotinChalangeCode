using Application.Common.Models;
using Application.Queries;
using Domain;

using MediatR;

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
    private readonly IMediator _mediator;

    public HomeController(ILogger<HomeController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost("MyData")]
    public Task<PaginatedList<MyData>> GetMyDataWithPagination(ISender sender, [AsParameters] GetMyDataWithPaginationQuery query)
    {
        return sender.Send(query);
    }
}
