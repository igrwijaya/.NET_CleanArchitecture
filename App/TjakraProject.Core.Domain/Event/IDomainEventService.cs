using System.Threading.Tasks;
using TjakraProject.Core.Domain.Common;

namespace TjakraProject.Core.Domain.Event
{
    public interface IDomainEventService
    {
        #region Public Methods

        Task Publish(DomainEvent domainEvent);

        #endregion
    }
}