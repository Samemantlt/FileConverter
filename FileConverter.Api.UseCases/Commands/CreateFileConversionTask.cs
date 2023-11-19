using FileConverter.Api.Domain.Models;
using FileConverter.Api.Domain.Repositories;
using MediatR;

namespace FileConverter.Api.UseCases.Commands;

public static class CreateFileConversionTask
{
    public record Request(byte[] InputFile) : IRequest<Response>;
    

    public record Response(Guid TaskId);


    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IConversionTaskRepository _conversionTaskRepository;

        
        public Handler(IConversionTaskRepository conversionTaskRepository)
        {
            _conversionTaskRepository = conversionTaskRepository;
        }
        
        
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var task = new BmpToJpegConversionTask(request.InputFile);
            
            await _conversionTaskRepository.Add(task);
            
            return new Response(task.Id);
        }
    }
}