using MultiShop.Mail.Dtos;

namespace MultiShop.Mail.Services
{
    public interface IMailService
    {
        void SendMail(SendMailDto sendMailDto);
    }
}
