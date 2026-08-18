using FluentValidation;
using MultiShop.Catalog.Dtos.ProductDetailDtos;

namespace MultiShop.Catalog.Validators
{
    public class CreateProductDetailDtoValidator : AbstractValidator<CreateProductDetailDto>
    {
        public CreateProductDetailDtoValidator()
        {
            RuleFor(x => x.ProductDescription).NotEmpty().WithMessage("Ürün açıklaması boş olamaz.");
            RuleFor(x => x.ProductInfo).NotEmpty().WithMessage("Ürün bilgisi boş olamaz.");
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Ürün Id boş olamaz.");
        }
    }

    public class UpdateProductDetailDtoValidator : AbstractValidator<UpdateProductDetailDto>
    {
        public UpdateProductDetailDtoValidator()
        {
            RuleFor(x => x.ProductDetailId).NotEmpty().WithMessage("Id boş olamaz.");
            RuleFor(x => x.ProductDescription).NotEmpty().WithMessage("Ürün açıklaması boş olamaz.");
            RuleFor(x => x.ProductInfo).NotEmpty().WithMessage("Ürün bilgisi boş olamaz.");
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Ürün Id boş olamaz.");
        }
    }
}
