using FileConverter.Worker.Infrastructure;
using FileConverter.Worker.Logic.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddInfrastructure();
builder.Services.AddSingleton<MagickNetImageConversionTaskExecutor>();

var app = builder.Build();

await app.RunAsync();