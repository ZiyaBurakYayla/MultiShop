using FluentValidation;
using MultiShop.Message.Dtos;

namespace MultiShop.Message.Validators
{
    public class CreateMessageDtoValidator : AbstractValidator<CreateMessageDto>
    {
        public CreateMessageDtoValidator()
        {
            RuleFor(x => x.SenderId).NotEmpty().WithMessage("Gönderen boş olamaz.");
            RuleFor(x => x.ReceiverId).NotEmpty().WithMessage("Alıcı boş olamaz.");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu boş olamaz.");
            RuleFor(x => x.MessageDetail).NotEmpty().WithMessage("Mesaj içeriği boş olamaz.");
            RuleFor(x => x.MessageDate).NotEmpty().WithMessage("Mesaj tarihi boş olamaz.");
        }
    }

    public class UpdateMessageDtoValidator : AbstractValidator<UpdateMessageDto>
    {
        public UpdateMessageDtoValidator()
        {
            RuleFor(x => x.UserMessageId).GreaterThan(0).WithMessage("Geçerli bir mesaj Id giriniz.");
            RuleFor(x => x.SenderId).NotEmpty().WithMessage("Gönderen boş olamaz.");
            RuleFor(x => x.ReceiverId).NotEmpty().WithMessage("Alıcı boş olamaz.");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu boş olamaz.");
            RuleFor(x => x.MessageDetail).NotEmpty().WithMessage("Mesaj içeriği boş olamaz.");
            RuleFor(x => x.MessageDate).NotEmpty().WithMessage("Mesaj tarihi boş olamaz.");
        }
    }
}
