namespace MultiShop.Cargo.DtoLayer.Dtos.CargoOperationDtos
{
    public class CreateCargoOperationDto
    {
        public string Description { get; set; }
        public string Barcode { get; set; }
        public DateTime OperationDate { get; set; }
    }
}
