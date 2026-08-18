using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.DataAccessLayer.Abstract
{
    public interface ICargoDetailDal : IGenericDal<CargoDetail>
    {
        List<CargoDetail> GetAllWithCompany();
        List<CargoDetail> GetByUserId(string userId);
        CargoDetail GetByBarcode(string barcode);
        List<CargoDetail> GetByCompanyId(int companyId);
    }
}
