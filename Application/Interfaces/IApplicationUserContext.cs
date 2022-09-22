using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationUserContext
    {
        DbSet<User> ApplicationUsers { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
