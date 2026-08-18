using Microsoft.EntityFrameworkCore;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Repositories;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.DataAccessLayer.EntityFramework
{
    public class EfCargoDetailDal : GenericRepository<CargoDetail>, ICargoDetailDal
    {
        private readonly CargoContext _context;

        public EfCargoDetailDal(CargoContext context) : base(context)
        {
            _context = context;
        }

        public List<CargoDetail> GetAllWithCompany()
        {
            return _context.CargoDetails.Include(x => x.CargoCompany).ToList();
        }

        public List<CargoDetail> GetByUserId(string userId)
        {
            return _context.CargoDetails.Include(x => x.CargoCompany)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CargoDetailId).ToList();
        }

        public CargoDetail GetByBarcode(string barcode)
        {
            return _context.CargoDetails.Include(x => x.CargoCompany)
                .FirstOrDefault(x => x.Barcode == barcode);
        }

        public List<CargoDetail> GetByCompanyId(int companyId)
        {
            return _context.CargoDetails.Include(x => x.CargoCompany)
                .Where(x => x.CargoCompanyId == companyId)
                .OrderByDescending(x => x.CargoDetailId).ToList();
        }
    }

}
