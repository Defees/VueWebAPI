using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IChatContext
    {
        DbSet<Chat> Chats { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
