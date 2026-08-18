using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.BusinessLayer.Abstract
{
    public interface ICargoOperationService : IGenericService<CargoOperation>
    {
        List<CargoOperation> TGetByBarcode(string barcode);
    }
}
