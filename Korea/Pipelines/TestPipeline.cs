using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Korea.Pipelines
{
    public class TestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {        
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Log.Information(Environment.NewLine);
            var response = await next();
            return response;
        }
    }
}