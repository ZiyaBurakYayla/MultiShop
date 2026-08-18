using MediatR;
using MultiShop.Order.Application.Features.Mediator.Queries.OrderDetailQueries;
using MultiShop.Order.Application.Features.Mediator.Results.OrderDetailResults;
using MultiShop.Order.Application.Interfaces;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderDetailHandlers
{
    public class GetOrderDetailsBySellerIdQueryHandler : IRequestHandler<GetOrderDetailsBySellerIdQuery, List<GetOrderDetailsBySellerIdResult>>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public GetOrderDetailsBySellerIdQueryHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<List<GetOrderDetailsBySellerIdResult>> Handle(GetOrderDetailsBySellerIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _orderDetailRepository.GetOrderDetailsBySellerId(request.SellerId);
            return values.Select(x => new GetOrderDetailsBySellerIdResult
            {
                OrderDetailId = x.OrderDetailId,
                OrderingId = x.OrderingId,
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                ProductPrice = x.ProductPrice,
                ProductAmount = x.ProductAmount,
                ProductTotalPrice = x.ProductTotalPrice,
                Sku = x.Sku,
                SellerId = x.SellerId,
                UserId = x.Ordering == null ? "" : x.Ordering.UserId,
                OrderDate = x.Ordering == null ? DateTime.MinValue : x.Ordering.OrderDate,
                IsPaid = x.Ordering != null && x.Ordering.IsPaid
            }).ToList();
        }
    }
}
