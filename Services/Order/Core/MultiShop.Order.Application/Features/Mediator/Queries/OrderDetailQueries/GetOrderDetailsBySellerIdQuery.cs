using MediatR;
using MultiShop.Order.Application.Features.Mediator.Results.OrderDetailResults;

namespace MultiShop.Order.Application.Features.Mediator.Queries.OrderDetailQueries
{
    public class GetOrderDetailsBySellerIdQuery : IRequest<List<GetOrderDetailsBySellerIdResult>>
    {
        public GetOrderDetailsBySellerIdQuery(string sellerId)
        {
            SellerId = sellerId;
        }

        public string SellerId { get; set; }
    }
}
