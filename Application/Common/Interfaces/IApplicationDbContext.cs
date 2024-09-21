using Domain;

using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<MyData> MyData { get; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
