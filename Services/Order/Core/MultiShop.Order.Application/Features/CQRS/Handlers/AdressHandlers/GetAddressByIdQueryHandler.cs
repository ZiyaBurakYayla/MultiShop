using MultiShop.Order.Application.Features.CQRS.Queries.AdressQueries;
using MultiShop.Order.Application.Features.CQRS.Results.AdressResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AdressHandlers
{
    public class GetAddressByIdQueryHandler
    {
        private readonly IRepository<Address> _addressRepository;

        public GetAddressByIdQueryHandler(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<GetAdressByIdQueryResult> HandleAsync(GetAddressByIdQuery query)
        {
            var value = await _addressRepository.GetByIdAsync(query.Id);
            return new GetAdressByIdQueryResult
            {
                City = value.City,
                Detail1 = value.Detail1,
                Detail2 = value.Detail2,
                District = value.District,
                UserId = value.UserId,
                Country = value.Country,
                Description = value.Description,
                PhoneNo = value.PhoneNo,
                Email = value.Email,
                Name = value.Name,
                Surname = value.Surname,
                ZipCode = value.ZipCode,
            };
        }
    }
}
