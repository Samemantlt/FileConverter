using FileConverter.Api.Domain.Core;
using FileConverter.Api.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FileConverter.Api.Database;

public class ConversionTaskContext : DbContext
{
    private readonly IMediator _mediator;
    public DbSet<FileConversionTask> FileConversionTasks => Set<FileConversionTask>();
    
    public DbSet<BmpToJpegConversionTask> BmpToJpegConversionTasks => Set<BmpToJpegConversionTask>();
    
    public DbSet<PngToJpegConversionTask> PngToJpegConversionTasks => Set<PngToJpegConversionTask>();
    
    
    public ConversionTaskContext(IMediator mediator, DbContextOptions<ConversionTaskContext> options) : base(options)
    {
        _mediator = mediator;
    }

    
    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is EntityBase entity)
                await entity.PublishAndClearEvents(_mediator);
        }
        
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}