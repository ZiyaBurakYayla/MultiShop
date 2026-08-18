namespace MultiShop.Cargo.DtoLayer.Dtos.CargoDetailDtos
{
    public class UpdateCargoDetailDto
    {
        public int CargoDetailId { get; set; }
        public string SenderCustomer { get; set; }
        public string ReceiverCustomer { get; set; }
        public string Barcode { get; set; }
        public int CargoCompanyId { get; set; }
        public int OrderingId { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
    }
}
