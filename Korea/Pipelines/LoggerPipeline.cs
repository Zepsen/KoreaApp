using MediatR;
using Serilog;
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
            Log.Warning(@"Request {0} begin", typeof(TRequest));
            var response = await next();
            Log.Warning(@"Response {0} end", typeof(TResponse));

            return response;
        }
    }
}