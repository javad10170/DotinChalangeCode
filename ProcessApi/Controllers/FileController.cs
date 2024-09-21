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
public class FileController : ControllerBase
{
    private readonly ILogger<FileController> _logger;
    private readonly IMediator _mediator;

    public FileController(ILogger<FileController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost("Content")]
    public Task<PaginatedList<Content>> GetContentWithPagination(ISender sender, [AsParameters] GetContentWithPaginationQuery query)
    {
        return sender.Send(query);
    }
}
