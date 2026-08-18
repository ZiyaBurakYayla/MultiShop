using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.DtoLayer.CatalogDtos.SellerDtos;
using MultiShop.DtoLayer.OrderDtos.OrderDetailDtos;

namespace MultiShop.WebUI.Models
{
    public class SellerDashboardViewModel
    {
        public ResultSellerDto Seller { get; set; }
        public List<ResultProductDto> Products { get; set; }
        public decimal PaidRevenue { get; set; }
        public decimal PendingRevenue { get; set; }
        public int OrderCount { get; set; }
        public int SoldQuantity { get; set; }
        public decimal AverageOrderValue { get; set; }
        public List<ResultOrderDetailBySellerDto> RecentOrders { get; set; }
        public List<SellerTopProductViewModel> TopProducts { get; set; }
    }

    public class SellerTopProductViewModel
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Revenue { get; set; }
    }
}
