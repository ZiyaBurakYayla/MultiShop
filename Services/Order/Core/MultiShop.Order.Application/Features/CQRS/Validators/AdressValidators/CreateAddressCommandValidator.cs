using FluentValidation;
using MultiShop.Order.Application.Features.CQRS.Commands.AdressCommands;

namespace MultiShop.Order.Application.Features.CQRS.Validators.AdressValidators
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Kullanıcı Id boş olamaz.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad boş olamaz.")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir.");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyad boş olamaz.")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-posta boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta giriniz.");
            RuleFor(x => x.PhoneNo).NotEmpty().WithMessage("Telefon numarası boş olamaz.");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Ülke boş olamaz.");
            RuleFor(x => x.City).NotEmpty().WithMessage("Şehir boş olamaz.");
            RuleFor(x => x.District).NotEmpty().WithMessage("İlçe boş olamaz.");
            RuleFor(x => x.Detail1).NotEmpty().WithMessage("Adres detayı boş olamaz.");
            RuleFor(x => x.ZipCode).NotEmpty().WithMessage("Posta kodu boş olamaz.");
        }
    }
}
