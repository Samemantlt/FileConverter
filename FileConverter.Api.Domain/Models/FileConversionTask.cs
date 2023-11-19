using FileConverter.Api.Domain.Core;

namespace FileConverter.Api.Domain.Models;

public abstract class FileConversionTask : EntityBase
{
    public Guid Id { get; private set; }
    
    public byte[] InputFile { get; private set; } = null!;
    
    public ConversionTaskStatus Status { get; private set; } = ConversionTaskStatus.Pending;
    
    public byte[]? OutputFile { get; private set; }
    
    public string? ErrorMessage { get; private set; }
    

    protected FileConversionTask() { }

    protected FileConversionTask(byte[] inputFile)
    {
        InputFile = inputFile ?? throw new ArgumentNullException(nameof(inputFile));
    }
    
    
    public void SetInProgress()
    {
        Status = ConversionTaskStatus.InProgress;
    }
    
    public void SetCompleted(byte[] outputFile)
    {
        OutputFile = outputFile;
        Status = ConversionTaskStatus.Completed;
    }
    
    public void SetError(string errorMessage)
    {
        Status = ConversionTaskStatus.Error;
        ErrorMessage = errorMessage;
    }
}