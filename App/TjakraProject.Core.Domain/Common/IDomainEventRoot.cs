using System.Collections.Generic;

namespace TjakraProject.Core.Domain.Common
{
    public interface IDomainEventRoot
    {
        #region Properties

        public List<DomainEvent> DomainEvents { get; set; }

        #endregion
    }
}