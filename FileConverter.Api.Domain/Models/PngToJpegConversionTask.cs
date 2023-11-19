namespace FileConverter.Api.Domain.Models;

public class PngToJpegConversionTask : FileConversionTask
{
    public PngToJpegConversionTask(byte[] inputFile) : base(inputFile) { }

    private PngToJpegConversionTask() { }
}