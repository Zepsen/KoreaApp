using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Handlers
{
    public class CategoryAllQuery
    {
        public class Request : IRequest<List<Category>>
        {
            public int Id { get; set; }
        }

        public class Category
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Image { get; set; }
        }

        //public class RequestValidator : AbstractValidator<Request>
        //{
        //    public RequestValidator()
        //    {
        //        RuleFor(x => x.Id).LessThan(1);                
        //    }
        //}

        public class CategoryAllQueryHandler : Query, IRequestHandler<Request, List<Category>>
        {
            public async Task<List<Category>> Handle(Request request, CancellationToken cancellationToken)
            {
                return await QueryAsync<Category>("spCategories_SelectAll", null);
            }
        }
    }
}
