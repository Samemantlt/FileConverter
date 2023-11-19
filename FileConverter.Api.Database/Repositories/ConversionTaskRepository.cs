using FileConverter.Api.Domain.Models;
using FileConverter.Api.Domain.Repositories;

namespace FileConverter.Api.Database.Repositories;

public class ConversionTaskRepository : IConversionTaskRepository
{
    private readonly ConversionTaskContext _context;

    
    public ConversionTaskRepository(ConversionTaskContext context)
    {
        _context = context;
    }
    
    
    public async Task Add(FileConversionTask task)
    {
        _context.FileConversionTasks.Add(task);
        await _context.SaveChangesAsync();
    }

    public async Task<FileConversionTask?> Get(Guid id)
    {
        return await _context.FileConversionTasks.FindAsync(id);
    }
}