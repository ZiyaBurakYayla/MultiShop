namespace MultiShop.Order.Application.RabbitMQ
{
    public class ProductStockReduceEvent
    {
        public string Sku { get; set; }
        public int Quantity { get; set; }
    }
}
