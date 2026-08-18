using FluentValidation;
using MultiShop.Catalog.Dtos.AboutDtos;

namespace MultiShop.Catalog.Validators
{
    public class CreateAboutDtoValidator : AbstractValidator<CreateAboutDto>
    {
        public CreateAboutDtoValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama boş olamaz.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Adres boş olamaz.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-posta boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta giriniz.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Telefon boş olamaz.");
        }
    }

    public class UpdateAboutDtoValidator : AbstractValidator<UpdateAboutDto>
    {
        public UpdateAboutDtoValidator()
        {
            RuleFor(x => x.AboutId).NotEmpty().WithMessage("Id boş olamaz.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama boş olamaz.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Adres boş olamaz.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-posta boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta giriniz.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Telefon boş olamaz.");
        }
    }
}
