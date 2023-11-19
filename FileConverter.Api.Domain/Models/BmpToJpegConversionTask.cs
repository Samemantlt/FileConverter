namespace FileConverter.Api.Domain.Models;

public class BmpToJpegConversionTask : FileConversionTask
{
    public BmpToJpegConversionTask(byte[] inputFile) : base(inputFile) { }

    private BmpToJpegConversionTask() { }
}