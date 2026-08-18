namespace MultiShop.IdentityServer.Dtos
{
    public class UpdateUserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
