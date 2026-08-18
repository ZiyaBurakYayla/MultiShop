namespace MultiShop.DtoLayer.CargoDtos.CargoOperationDtos
{
    public class ResultCargoOperationDto
    {
        public int CargoOperationId { get; set; }
        public string Description { get; set; }
        public string Barcode { get; set; }
        public DateTime OperationDate { get; set; }
    }
}
