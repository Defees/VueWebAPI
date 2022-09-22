using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IFriendshipContext
    {
        DbSet<Friendship> Friendships { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
