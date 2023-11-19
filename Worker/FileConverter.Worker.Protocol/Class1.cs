namespace FileConverter.Worker.Protocol;

public enum ImageFormat
{
    Jpeg,
    Png,
    Bmp
}

public record ImageConversionTask(
    Guid Id,
    byte[] InputFile,
    ImageFormat InputFormat,
    ImageFormat TargetFormat
);

public static class WorkerConstants
{
    public const string ImageConversionTaskTopicName = "image-conversion-task";
}