using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Repositories;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.DataAccessLayer.EntityFramework
{
    public class EfCargoOperationDal : GenericRepository<CargoOperation>, ICargoOperationDal
    {
        private readonly CargoContext _context;

        public EfCargoOperationDal(CargoContext context) : base(context)
        {
            _context = context;
        }
        public List<CargoOperation> GetByBarcode(string barcode)
        {
            return _context.CargoOperations
    .Where(x => x.Barcode == barcode)
    .OrderBy(x => x.OperationDate).ToList();
        }
    }
}
