using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;

using Domain;

using MediatR;

using System;

namespace Application.Queries
{
    public record GetMyDataWithPaginationQuery : IRequest<PaginatedList<MyData>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetMyDataWithPaginationHandler : IRequestHandler<GetMyDataWithPaginationQuery, PaginatedList<MyData>>
    {
        private readonly IApplicationDbContext _context;

        public GetMyDataWithPaginationHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<MyData>> Handle(GetMyDataWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.MyData
                .Where(x=>x.Id > 0)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
