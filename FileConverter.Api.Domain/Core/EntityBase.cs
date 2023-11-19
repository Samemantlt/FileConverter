using System.Text.Json.Serialization;
using MediatR;

namespace FileConverter.Api.Domain.Core;

public class EntityBase
{
    [JsonIgnore]
    public IReadOnlyCollection<IDomainEvent> Events => _events.AsReadOnly();


    public void AddEvent(IDomainEvent @event)
    {
        _events.Add(@event);
    }

    public async Task PublishAndClearEvents(IMediator mediator)
    {
        foreach (var @event in _events)
        {
            await mediator.Publish(@event);
        }

        _events.Clear();
    }


    private readonly List<IDomainEvent> _events = new List<IDomainEvent>();
}

public interface IBrokerEvent : IDomainEvent { }

public interface IDomainEvent : INotification { }