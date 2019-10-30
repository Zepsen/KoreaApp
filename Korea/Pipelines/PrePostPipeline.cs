using MediatR;
using MediatR.Pipeline;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Korea.Pipelines
{
    public class PreProcessorBehavior<TRequest> : IRequestPreProcessor<TRequest>        
    {
        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            Log.Error(Environment.NewLine);
            Log.Error("Pre");
        }
    }

    public class PostProcessorBehavior<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {         
        public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            Log.Error("Post" + Environment.NewLine);
        }
    }
}