using Microsoft.EntityFrameworkCore;
using MultiShop.Message.Dal.Context;

namespace MultiShop.Message.Services.StatisticServices
{
    public class StatisticService : IStatisticService
    {
        private readonly MessageContext _messageContext;

        public StatisticService(MessageContext messageContext)
        {
            _messageContext = messageContext;
        }

        public async Task<int> GetTotalMessageCountAsync()
        {
            return await _messageContext.UserMessages.CountAsync();
        }
        public async Task<int> GetTotalMessageCountByReceiverId(string id)
        {
            var values = await _messageContext.UserMessages.Where(x => x.ReceiverId == id).CountAsync();
            return values;
        }
    }
}
