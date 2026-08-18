using MediatR;
using MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries;
using MultiShop.Order.Application.Features.Mediator.Results.OrderingResults;
using MultiShop.Order.Application.Interfaces;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    public class GetOrderingByUserIdQueryHandler : IRequestHandler<GetOrderingByUserIdQuery, List<GetOrderingByUserIdResult>>
    {
        private readonly IOrderingRepository _orderingRepository;

        public GetOrderingByUserIdQueryHandler(IOrderingRepository orderingRepository)
        {
            _orderingRepository = orderingRepository;
        }

        public async Task<List<GetOrderingByUserIdResult>> Handle(GetOrderingByUserIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _orderingRepository.GetOrderingByUserId(request.Id);
            return values.Select(x => new GetOrderingByUserIdResult
            {
                OrderDate = x.OrderDate,
                UserId = x.UserId,
                OrderingId = x.OrderingId,
                TotalPrice = x.TotalPrice,
                IsPaid = x.IsPaid,
            }).ToList();
        }
    }
}
