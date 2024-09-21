using Application.Common.Interfaces;

using MediatR;
using Microsoft.AspNetCore.Http;

using System;
using System.Reflection.Metadata;

namespace Application.Command
{
    public class UploadFileCommand : IRequest
    {
        public IFormFile File { get; set; }
    }

    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand>
    {
        private readonly IRabbitMqService _rabbitMqService;

        public UploadFileCommandHandler(IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        public async Task Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            using var stream = new MemoryStream();
            request.File.CopyTo(stream);
            using var reader = new StreamReader(request.File.OpenReadStream());

            // ارسال فایل به RabbitMQ
            using var connection = _rabbitMqService.CreateChannel();
            using var model = connection.CreateModel();
            model.BasicPublish("ProcessFile", string.Empty, false, basicProperties: null, body: stream.ToArray());
        }
    }
}
