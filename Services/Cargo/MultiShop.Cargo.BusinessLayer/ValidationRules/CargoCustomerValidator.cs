using FluentValidation;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCustomerDtos;

namespace MultiShop.Cargo.BusinessLayer.ValidationRules
{
    public class CreateCargoCustomerValidator : AbstractValidator<CreateCargoCustomerDto>
    {
        public CreateCargoCustomerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad boş olamaz.");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyad boş olamaz.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-posta boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta giriniz.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Telefon boş olamaz.");
            RuleFor(x => x.District).NotEmpty().WithMessage("İlçe boş olamaz.");
            RuleFor(x => x.City).NotEmpty().WithMessage("Şehir boş olamaz.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Adres boş olamaz.");
            RuleFor(x => x.UserCustomerId).NotEmpty().WithMessage("Kullanıcı Id boş olamaz.");
        }
    }

    public class UpdateCargoCustomerValidator : AbstractValidator<UpdateCargoCustomerDto>
    {
        public UpdateCargoCustomerValidator()
        {
            RuleFor(x => x.CargoCustomerId).GreaterThan(0).WithMessage("Geçerli bir müşteri Id giriniz.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad boş olamaz.");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyad boş olamaz.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-posta boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta giriniz.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Telefon boş olamaz.");
            RuleFor(x => x.District).NotEmpty().WithMessage("İlçe boş olamaz.");
            RuleFor(x => x.City).NotEmpty().WithMessage("Şehir boş olamaz.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Adres boş olamaz.");
            RuleFor(x => x.UserCustomerId).NotEmpty().WithMessage("Kullanıcı Id boş olamaz.");
        }
    }
}
