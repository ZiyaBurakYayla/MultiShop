using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.BusinessLayer.Abstract
{
    public interface ICargoDetailService : IGenericService<CargoDetail>
    {
        List<CargoDetail> TGetAllWithCompany();
        List<CargoDetail> TGetByUserId(string userId);
        CargoDetail TGetByBarcode(string barcode);
        List<CargoDetail> TGetByCompanyId(int companyId);
    }
}
