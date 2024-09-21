using Application.Command;
using Application.Common.Models;
using Application.Queries;

using Domain;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace DotinChalangeCode.Controllers;

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

    [HttpPost("UploadExcel")]
    public async Task<IActionResult> UploadExcel(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var command = new UploadFileCommand { File = file };
        await _mediator.Send(command);

        return Ok("file upload in queue for process.");
    }

}
