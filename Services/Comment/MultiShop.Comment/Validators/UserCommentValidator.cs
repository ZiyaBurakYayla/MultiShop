using FluentValidation;
using MultiShop.Comment.Entities;

namespace MultiShop.Comment.Validators
{
    public class UserCommentValidator : AbstractValidator<UserComment>
    {
        public UserCommentValidator()
        {
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Ad soyad boş olamaz.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-posta boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta giriniz.");
            RuleFor(x => x.CommentDetail).NotEmpty().WithMessage("Yorum boş olamaz.")
                .MaximumLength(1000).WithMessage("Yorum en fazla 1000 karakter olabilir.");
            RuleFor(x => x.Rating).InclusiveBetween(1, 5).WithMessage("Puan 1 ile 5 arasında olmalı.");
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Ürün Id boş olamaz.");
        }
    }
}
