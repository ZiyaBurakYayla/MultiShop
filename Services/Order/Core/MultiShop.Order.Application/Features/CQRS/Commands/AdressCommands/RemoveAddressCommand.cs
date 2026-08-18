namespace MultiShop.Order.Application.Features.CQRS.Commands.AdressCommands
{
    public class RemoveAddressCommand
    {
        public int AddressId { get; set; }
        public RemoveAddressCommand(int addressId)
        {
            AddressId = addressId;
        }
    }
}
