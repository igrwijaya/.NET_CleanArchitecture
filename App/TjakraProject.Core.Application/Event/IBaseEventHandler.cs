using MediatR;
using TjakraProject.Core.Application.Event.Notification;
using TjakraProject.Core.Domain.Common;

namespace TjakraProject.Core.Application.Event
{
    public interface IBaseEventHandler<TDomainEvent> : INotificationHandler<DomainEventNotification<TDomainEvent>> 
        where TDomainEvent : DomainEvent
    {

    }
}