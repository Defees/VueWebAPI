using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IMessageContext
    {
        DbSet<Message> Messages { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
