using System;

namespace TjakraProject.Core.Domain.Common
{
    public class DomainEvent
    {
        #region Constructors

        protected DomainEvent()
        {
            DateOccurred = DateTimeOffset.UtcNow;
        }

        #endregion

        #region Properties

        public bool IsPublished { get; set; }
        
        public DateTimeOffset DateOccurred { get; protected set; }

        #endregion

    }
}