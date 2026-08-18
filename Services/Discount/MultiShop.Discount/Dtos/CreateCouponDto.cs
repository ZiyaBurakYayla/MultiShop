namespace MultiShop.Discount.Dtos
{
    public class CreateCouponDto
    {
        public string CouponCode { get; set; }
        public int CouponRate { get; set; }
        public bool IsAcvtive { get; set; }
        public DateTime ValidDate { get; set; }
    }
}
