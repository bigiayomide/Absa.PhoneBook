using Absa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Absa.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {

        DbSet<Absa.Domain.Entities.PhoneBook> PhoneBooks { get; set; }

        DbSet<Absa.Domain.Entities.PhoneBookEntry> PhoneBookEntries { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
