using FileConverter.Api.Domain.Core;
using FileConverter.Api.Domain.Models;

namespace FileConverter.Api.Domain.Events;

public interface IWorkerEvent : IBrokerEvent { }

public record AddFileConversionTaskWorkerEvent(
    FileConversionTask FileConversionTask
) : IWorkerEvent;