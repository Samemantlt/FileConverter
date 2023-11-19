namespace FileConverter.Worker.Logic.Services;

public interface IEventSource<out T> where T : class
{
    IAsyncEnumerable<T> EnumerateEvents(CancellationToken cancellationToken = default);
}