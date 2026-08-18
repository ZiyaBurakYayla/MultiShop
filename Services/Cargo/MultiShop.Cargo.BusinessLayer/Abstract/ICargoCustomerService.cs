using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.BusinessLayer.Abstract
{
    public interface ICargoCustomerService : IGenericService<CargoCustomer>
    {
        Task<CargoCustomer> TGetCargoCustomerByIdAsync(string id);
    }
}
