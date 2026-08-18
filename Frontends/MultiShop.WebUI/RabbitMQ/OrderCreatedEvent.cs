namespace MultiShop.WebUI.RabbitMQ
{
    public class OrderCreatedEvent
    {
        public string CustomerEmail { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalPrice { get; set; }
        public string TransactionId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderCreatedItem> Items { get; set; }
    }

    public class OrderCreatedItem
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
