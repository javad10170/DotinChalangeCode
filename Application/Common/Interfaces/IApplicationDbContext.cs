using Domain;

using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Content> Contents { get; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
