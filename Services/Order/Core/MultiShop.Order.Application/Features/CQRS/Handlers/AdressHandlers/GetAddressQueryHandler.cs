using MultiShop.Order.Application.Features.CQRS.Results.AdressResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
namespace MultiShop.Order.Application.Features.CQRS.Handlers.AdressHandlers
{
    public class GetAddressQueryHandler
    {
        private readonly IRepository<Address> _addressRepository;

        public GetAddressQueryHandler(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<List<GetAdressQueryResult>> HandleAsync()
        {
            var value = await _addressRepository.GetAllAsync();
            return value.Select(x => new GetAdressQueryResult
            {
                City = x.City,
                Detail1 = x.Detail1,
                Detail2 = x.Detail2,
                District = x.District,
                UserId = x.UserId,
                Country = x.Country,
                Description = x.Description,
                PhoneNo = x.PhoneNo,
                Email = x.Email,
                Name = x.Name,
                Surname = x.Surname,
                ZipCode = x.ZipCode,
            }).ToList();
        }
    }
}
