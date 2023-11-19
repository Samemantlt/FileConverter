using MediatR;

namespace FileConverter.Worker.Logic.Models;

public interface IDomainEvent : INotification { }

public interface IBrokerEvent : IDomainEvent { }

public record SetTaskInProgressEvent(
    Guid TaskId
) : IBrokerEvent;

public record SetTaskCompletedEvent(
    Guid TaskId,
    byte[] OutputFile
) : IBrokerEvent;

public record SetTaskErrorOccurredEvent(
    Guid TaskId,
    string ErrorMessage
) : IBrokerEvent;