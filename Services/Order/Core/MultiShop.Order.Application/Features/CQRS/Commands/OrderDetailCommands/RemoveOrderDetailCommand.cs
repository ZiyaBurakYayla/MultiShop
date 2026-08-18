namespace MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands
{
    public class RemoveOrderDetailCommand
    {
        public int OrderDetailId { get; set; }
        public RemoveOrderDetailCommand(int orderDetailId)
        {
            OrderDetailId = orderDetailId;
        }
    }
}
