using FluentValidation;
using MultiShop.Catalog.Dtos.ProductDtos;

namespace MultiShop.Catalog.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Ürün adı boş olamaz.")
                .MaximumLength(200).WithMessage("Ürün adı en fazla 200 karakter olabilir.");
            RuleFor(x => x.ProductPrice).GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalı.");
            RuleFor(x => x.ProductImageUrl).NotEmpty().WithMessage("Ürün görseli boş olamaz.");
            RuleFor(x => x.ProductDescription).NotEmpty().WithMessage("Ürün açıklaması boş olamaz.");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Kategori seçilmelidir.");
            RuleForEach(x => x.Variants).SetValidator(new ProductVariantDtoValidator());
        }
    }

    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Id boş olamaz.");
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Ürün adı boş olamaz.")
                .MaximumLength(200).WithMessage("Ürün adı en fazla 200 karakter olabilir.");
            RuleFor(x => x.ProductPrice).GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalı.");
            RuleFor(x => x.ProductImageUrl).NotEmpty().WithMessage("Ürün görseli boş olamaz.");
            RuleFor(x => x.ProductDescription).NotEmpty().WithMessage("Ürün açıklaması boş olamaz.");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Kategori seçilmelidir.");
            RuleForEach(x => x.Variants).SetValidator(new ProductVariantDtoValidator());
        }
    }

    public class ProductVariantDtoValidator : AbstractValidator<ProductVariantDto>
    {
        public ProductVariantDtoValidator()
        {
            RuleFor(x => x.Sku).NotEmpty().WithMessage("SKU boş olamaz.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Varyant fiyatı 0'dan büyük olmalı.");
            RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("Stok negatif olamaz.");
        }
    }
}
