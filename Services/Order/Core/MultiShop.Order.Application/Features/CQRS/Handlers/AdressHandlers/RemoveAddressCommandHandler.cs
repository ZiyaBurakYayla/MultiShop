using MultiShop.Order.Application.Features.CQRS.Commands.AdressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AdressHandlers
{
    public class RemoveAddressCommandHandler
    {
        private readonly IRepository<Address> _addressRepository;
        public RemoveAddressCommandHandler(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task HandleAsync(RemoveAddressCommand removeAddressCommand)
        {
            var address = await _addressRepository.GetByIdAsync(removeAddressCommand.AddressId);
            await _addressRepository.DeleteAsync(address);
        }
    }
}
