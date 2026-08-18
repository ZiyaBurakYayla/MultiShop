namespace MultiShop.DtoLayer.OrderDtos.OrderDetailDtos
{
    public class ResultOrderDetailBySellerDto
    {
        public int OrderDetailId { get; set; }
        public int OrderingId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductAmount { get; set; }
        public decimal ProductTotalPrice { get; set; }
        public string Sku { get; set; }
        public string SellerId { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsPaid { get; set; }
    }
}
