using FluentValidation;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCompanyDtos;

namespace MultiShop.Cargo.BusinessLayer.ValidationRules
{
    public class CreateCargoCompanyValidator : AbstractValidator<CreateCargoCompanyDto>
    {
        public CreateCargoCompanyValidator()
        {
            RuleFor(x => x.CargoCompanyName).NotEmpty().WithMessage("Kargo firma adı boş olamaz.")
                .MaximumLength(100).WithMessage("Kargo firma adı en fazla 100 karakter olabilir.");
        }
    }

    public class UpdateCargoCompanyValidator : AbstractValidator<UpdateCargoCompanyDto>
    {
        public UpdateCargoCompanyValidator()
        {
            RuleFor(x => x.CargoCompanyId).GreaterThan(0).WithMessage("Geçerli bir firma Id giriniz.");
            RuleFor(x => x.CargoCompanyName).NotEmpty().WithMessage("Kargo firma adı boş olamaz.")
                .MaximumLength(100).WithMessage("Kargo firma adı en fazla 100 karakter olabilir.");
        }
    }
}
