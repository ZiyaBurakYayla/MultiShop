using FluentValidation;
using MultiShop.Cargo.DtoLayer.Dtos.CargoDetailDtos;

namespace MultiShop.Cargo.BusinessLayer.ValidationRules
{
    public class CreateCargoDetailValidator : AbstractValidator<CreateCargoDetailDto>
    {
        public CreateCargoDetailValidator()
        {
            RuleFor(x => x.SenderCustomer).NotEmpty().WithMessage("Gönderici boş olamaz.");
            RuleFor(x => x.ReceiverCustomer).NotEmpty().WithMessage("Alıcı boş olamaz.");
            RuleFor(x => x.Barcode).NotEmpty().WithMessage("Barkod boş olamaz.");
            RuleFor(x => x.CargoCompanyId).GreaterThan(0).WithMessage("Kargo firması seçilmelidir.");
        }
    }

    public class UpdateCargoDetailValidator : AbstractValidator<UpdateCargoDetailDto>
    {
        public UpdateCargoDetailValidator()
        {
            RuleFor(x => x.CargoDetailId).GreaterThan(0).WithMessage("Geçerli bir detay Id giriniz.");
            RuleFor(x => x.SenderCustomer).NotEmpty().WithMessage("Gönderici boş olamaz.");
            RuleFor(x => x.ReceiverCustomer).NotEmpty().WithMessage("Alıcı boş olamaz.");
            RuleFor(x => x.Barcode).NotEmpty().WithMessage("Barkod boş olamaz.");
            RuleFor(x => x.CargoCompanyId).GreaterThan(0).WithMessage("Kargo firması seçilmelidir.");
        }
    }
}
