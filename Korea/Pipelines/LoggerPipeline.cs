using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Korea.Pipelines
{
    public class LoggerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public LoggerBehavior()
        {

        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Log.Information(Environment.NewLine);
            Log.Warning(@"Request {0} begin", typeof(TRequest).Name);

            var response = await next();

            Log.Warning(@"Response {0} end" + Environment.NewLine, typeof(TResponse).Name);

            return response;
        }
    }
}