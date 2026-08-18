using Microsoft.EntityFrameworkCore;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Repositories;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.DataAccessLayer.EntityFramework
{
    public class EfCargoCustomerDal : GenericRepository<CargoCustomer>, ICargoCustomerDal
    {
        private readonly CargoContext _context;
        public EfCargoCustomerDal(CargoContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CargoCustomer> GetCargoCustomerByIdAsync(string id)
        {
           var value = await _context.CargoCustomers.Where(z => z.UserCustomerId == id).FirstOrDefaultAsync();
           return value;
        }
    }
}
