using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly DapperContext _context;

        public DiscountService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateCouponAsync(CreateCouponDto createCouponDto)
        {
            string query = "insert into coupons (CouponCode,CouponRate,IsAcvtive,ValidDate) values " +
                "(@code,@rate,@ısActive,@validDate)";
            var parameters = new DynamicParameters();
            parameters.Add("code", createCouponDto.CouponCode);
            parameters.Add("rate", createCouponDto.CouponRate);
            parameters.Add("ısActive", createCouponDto.IsAcvtive);
            parameters.Add("validDate", createCouponDto.ValidDate);
            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteCouponAsync(int couponId)
        {
            string query = "delete from coupons where CouponId=@id";
            var parameters = new DynamicParameters();
            parameters.Add("id", couponId);
            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultCouponDto>> GetAllCouponAsync()
        {
            string query = "select * from coupons";
            using(var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<ResultCouponDto>(query);
                return result.ToList();
            }
        }

        public async Task<GetByIdCouponDto> GetByIdCouponAsync(int couponId)
        {
            string query = "select * from coupons where CouponId=@id";
            var parameters = new DynamicParameters();
            parameters.Add("id", couponId);
            using(var connection = _context.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<GetByIdCouponDto>(query, parameters);
                return result;
            }
        }

        public async Task<ResultCouponDto> GetCodeDetailByCodeAsync(string code)
        {
            string query = "select * from coupons where CouponCode=@CouponCode";
            var parameters = new DynamicParameters();
            parameters.Add("CouponCode", code);
            using( var connection = _context.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<ResultCouponDto>(query,parameters);
                return result;
            }

        }

        public int GetDiscountCouponRate(string code)
        {
            string query = "select CouponRate from Coupons Where CouponCode= @code";
            var parameters = new DynamicParameters();
            parameters.Add("code",code);
            using (var connection = _context.CreateConnection())
            {
                var result = connection.QueryFirstOrDefault<int>(query, parameters);
                return result;
            }
        }

        public async Task UpdateCouponAsync(UpdateCouponDto updateCouponDto)
        {
            string query = "update coupons set CouponCode=@code,CouponRate=@rate,IsAcvtive=@ısActive,ValidDate=@validDate where CouponId=@id";
            var parameters = new DynamicParameters();
            parameters.Add("code", updateCouponDto.CouponCode);
            parameters.Add("rate", updateCouponDto.CouponRate);
            parameters.Add("ısActive", updateCouponDto.IsAcvtive);
            parameters.Add("validDate", updateCouponDto.ValidDate);
            parameters.Add("id", updateCouponDto.CouponId);
            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
