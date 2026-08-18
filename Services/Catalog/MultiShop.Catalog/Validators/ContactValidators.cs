using FluentValidation;
using MultiShop.Catalog.Dtos.ContactDtos;

namespace MultiShop.Catalog.Validators
{
    public class CreateContactDtoValidator : AbstractValidator<CreateContactDto>
    {
        public CreateContactDtoValidator()
        {
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Ad soyad boş olamaz.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-posta boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta giriniz.");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu boş olamaz.");
            RuleFor(x => x.Message).NotEmpty().WithMessage("Mesaj boş olamaz.");
        }
    }

    public class UpdateContactDtoValidator : AbstractValidator<UpdateContactDto>
    {
        public UpdateContactDtoValidator()
        {
            RuleFor(x => x.ContactId).NotEmpty().WithMessage("Id boş olamaz.");
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Ad soyad boş olamaz.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-posta boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta giriniz.");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu boş olamaz.");
            RuleFor(x => x.Message).NotEmpty().WithMessage("Mesaj boş olamaz.");
        }
    }
}
