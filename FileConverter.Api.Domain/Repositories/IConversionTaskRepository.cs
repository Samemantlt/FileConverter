using FileConverter.Api.Domain.Models;

namespace FileConverter.Api.Domain.Repositories;

public interface IConversionTaskRepository
{
    Task Add(FileConversionTask task);
    
    Task<FileConversionTask?> Get(Guid id);
}