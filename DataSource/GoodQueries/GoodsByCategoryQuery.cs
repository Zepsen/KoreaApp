﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SharedModels;

namespace Handlers
{
    public class GoodsByCategoryQuery
    {
        public class Request : IRequest<Result<Good>>
        {
            public int CategoryId { get; set; }
            public int Take { get; set; }
            public int Skip { get; set; }               
        }

        public class Good
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Image { get; set; }
            public decimal Price { get; set; }
            public int CategoryId { get; set; }
        }

        public class GoodsByCategoryQueryHandler : Query, IRequestHandler<Request, Result<Good>>
        {
            public async Task<Result<Good>> Handle(Request request, CancellationToken cancellationToken)
            {                
                return await QueryMultipleAsync<Good>("spGoodsByCategory_SelectAll", request);
            }
        }
    }
}