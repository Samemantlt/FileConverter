using FileConverter.Worker.Logic.Models;
using FileConverter.Worker.Protocol;
using ImageMagick;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FileConverter.Worker.Logic.Services;

public class MagickNetImageConversionTaskExecutor
{
    public MagickNetImageConversionTaskExecutor(IMediator mediator, IEventSource<ImageConversionTask> eventSource, ILogger<MagickNetImageConversionTaskExecutor> logger)
    {
        _mediator = mediator;
        _eventSource = eventSource;
        _logger = logger;
    }

    
    public async Task Execute(ImageConversionTask task)
    {
        try
        {
            await ExecuteInternal(task);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in {TypeName}.Execute", GetType().Name);
            await _mediator.Send(new SetTaskErrorOccurredEvent(task.Id, "Internal error"));
        }
    }

    
    private async Task ExecuteInternal(ImageConversionTask task)
    {
        await _mediator.Send(new SetTaskInProgressEvent(task.Id));

        using var image = new MagickImage(task.InputFile, GetFormat(task.InputFormat));
        
        image.Format = GetFormat(task.TargetFormat);
        
        using var mem = new MemoryStream();
        
        // ReSharper disable once MethodHasAsyncOverload
        // Reason: MemoryStream does not need async
        image.Write(mem);
        
        await _mediator.Send(new SetTaskCompletedEvent(task.Id, mem.ToArray()));
    }

    private static MagickFormat GetFormat(ImageFormat imageFormat)
    {
        return imageFormat switch
        {
            ImageFormat.Bmp => MagickFormat.Bmp,
            ImageFormat.Jpeg => MagickFormat.Jpeg,
            ImageFormat.Png => MagickFormat.Png,
            _ => throw new ArgumentException($"Unknown image format {imageFormat}. Int: {(int)imageFormat}")
        };
    }
    
    
    private readonly IMediator _mediator;
    private readonly IEventSource<ImageConversionTask> _eventSource;
    private readonly ILogger<MagickNetImageConversionTaskExecutor> _logger;
}