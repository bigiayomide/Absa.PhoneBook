using Absa.Domain.Common;
using System.Threading.Tasks;

namespace Absa.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
