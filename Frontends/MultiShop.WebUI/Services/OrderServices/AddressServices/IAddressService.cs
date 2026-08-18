using MultiShop.DtoLayer.OrderDtos.AddressDtos;

namespace MultiShop.WebUI.Services.OrderServices.AddressServices
{
    public interface IAddressService
    {
        Task CreateAddressAsync(CreateAddressDto addressDto);
    }
}
