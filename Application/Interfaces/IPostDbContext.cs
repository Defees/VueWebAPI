using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IPostDbContext
    {
        DbSet<Post> Posts { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
