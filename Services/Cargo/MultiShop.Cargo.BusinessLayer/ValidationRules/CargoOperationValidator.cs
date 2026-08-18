using FluentValidation;
using MultiShop.Cargo.DtoLayer.Dtos.CargoOperationDtos;

namespace MultiShop.Cargo.BusinessLayer.ValidationRules
{
    public class CreateCargoOperationValidator : AbstractValidator<CreateCargoOperationDto>
    {
        public CreateCargoOperationValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama boş olamaz.");
            RuleFor(x => x.Barcode).NotEmpty().WithMessage("Barkod boş olamaz.");
            RuleFor(x => x.OperationDate).NotEmpty().WithMessage("İşlem tarihi boş olamaz.");
        }
    }

    public class UpdateCargoOperationValidator : AbstractValidator<UpdateCargoOperationDto>
    {
        public UpdateCargoOperationValidator()
        {
            RuleFor(x => x.CargoOperationId).GreaterThan(0).WithMessage("Geçerli bir işlem Id giriniz.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama boş olamaz.");
            RuleFor(x => x.Barcode).NotEmpty().WithMessage("Barkod boş olamaz.");
            RuleFor(x => x.OperationDate).NotEmpty().WithMessage("İşlem tarihi boş olamaz.");
        }
    }
}
