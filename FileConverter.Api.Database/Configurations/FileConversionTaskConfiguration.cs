using FileConverter.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileConverter.Api.Database.Configurations;

public class FileConversionTaskConfiguration : IEntityTypeConfiguration<FileConversionTask>
{
    public void Configure(EntityTypeBuilder<FileConversionTask> builder)
    {
        builder.UseTpcMappingStrategy();
    }
}