using MediatR;
using MultiShop.Order.Application.Features.Mediator.Results.OrderingResults;

namespace MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries
{
    public class GetOrderingByUserIdQuery : IRequest<List<GetOrderingByUserIdResult>>
    {
        public GetOrderingByUserIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }

    }
}
