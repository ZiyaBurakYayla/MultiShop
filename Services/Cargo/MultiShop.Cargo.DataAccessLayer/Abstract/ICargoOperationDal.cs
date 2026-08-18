using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.DataAccessLayer.Abstract
{
    public interface ICargoOperationDal : IGenericDal<CargoOperation>
    {
        List<CargoOperation> GetByBarcode(string barcode);
    }
}
