using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;

using Domain;

using MediatR;

using System;

namespace Application.Queries
{
    public record GetContentWithPaginationQuery : IRequest<PaginatedList<Content>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetContentWithPaginationHandler : IRequestHandler<GetContentWithPaginationQuery, PaginatedList<Content>>
    {
        private readonly IApplicationDbContext _context;

        public GetContentWithPaginationHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Content>> Handle(GetContentWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Contents
                .Where(x=>x.Id > 0)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
