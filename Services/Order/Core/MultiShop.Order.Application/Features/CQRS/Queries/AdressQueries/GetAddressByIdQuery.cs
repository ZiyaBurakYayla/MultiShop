namespace MultiShop.Order.Application.Features.CQRS.Queries.AdressQueries
{
    public class GetAddressByIdQuery
    {
        public int Id { get; set; }
        public GetAddressByIdQuery(int id)
        {
            Id = id;
        }
    }
}
