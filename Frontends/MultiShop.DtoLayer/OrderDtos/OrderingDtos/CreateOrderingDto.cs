using System;

namespace MultiShop.DtoLayer.OrderDtos.OrderingDtos
{
    public class CreateOrderingDto
    {
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsPaid { get; set; }
    }
}
