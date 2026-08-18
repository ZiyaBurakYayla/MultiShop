using Microsoft.EntityFrameworkCore;
using MultiShop.Message.Dal.Entities;

namespace MultiShop.Message.Dal.Context
{
    public class MessageContext : DbContext
    {
        public MessageContext(DbContextOptions<MessageContext> options) : base(options)
        {

        }
        public DbSet<UserMessage> UserMessages { get; set; }
    }
}
