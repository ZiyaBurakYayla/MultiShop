namespace MultiShop.DtoLayer.DiscountDtos
{
    public class GetDiscountCodeDetailByCodeDto
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public int CouponRate { get; set; }
        public bool IsAcvtive { get; set; }
        public DateTime ValidDate { get; set; }
    }
}
