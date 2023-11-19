using System.Runtime.CompilerServices;
using System.Text.Json;
using Confluent.Kafka;
using FileConverter.Worker.Logic.Services;
using FileConverter.Worker.Protocol;
using Microsoft.Extensions.Logging;

namespace FileConverter.Worker.Infrastructure;

public class KafkaEventSource<T> : IEventSource<T> where T : class
{
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly ILogger<KafkaEventSource<T>> _logger;


    public KafkaEventSource(string topicName, ConsumerConfig consumerConfig, ILogger<KafkaEventSource<T>> logger)
    {
        _logger = logger;

        var builder = new ConsumerBuilder<Ignore, string>(consumerConfig);
        _consumer = builder.Build();
        _consumer.Subscribe(topicName);
    }


    public async IAsyncEnumerable<T> EnumerateEvents(
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var message = await ReadNoThrow(cancellationToken);
            if (message != null)
                yield return message;
        }

        cancellationToken.ThrowIfCancellationRequested();
    }


    private async Task<T?> ReadNoThrow(CancellationToken cancellationToken)
    {
        try
        {
            var result = await Task.Run(() => _consumer.Consume(cancellationToken), cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();

            var json = result.Message.Value;
            if (string.IsNullOrEmpty(json))
            {
                _logger.LogError("Could not deserialize message. `Message.Value` is null.");
                return null;
            }

            var task = JsonSerializer.Deserialize<T>(json);
            if (task == null)
            {
                _logger.LogError("Could not deserialize message. Json deserialization returned null.");
                return null;
            }

            return task;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in {nameof(KafkaEventSource<T>)}.{nameof(ReadNoThrow)}");
            await Task.Delay(1000, cancellationToken);
            return null;
        }
    }
}