namespace CustomCADs.Shared.Application.Abstractions.Events;

public interface IEventHandler<TEvent> where TEvent : BaseEvent
{
	Task HandleAsync(TEvent @event);
}
