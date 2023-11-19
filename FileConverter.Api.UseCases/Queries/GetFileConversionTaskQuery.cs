using FileConverter.Api.Domain.Models;
using FileConverter.Api.Domain.Repositories;
using MediatR;

namespace FileConverter.Api.UseCases.Queries;

public static class GetFileConversionTaskQuery
{
    public record Request(Guid TaskId) : IRequest<Response>;
    
    public record Response(FileConversionTask? ConversionTask);
    
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IConversionTaskRepository _taskRepository;

        
        public Handler(IConversionTaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        
        
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.Get(request.TaskId);
            
            if (task == null)
                return new Response(null);
            
            return new Response(task);
        }
    }
}