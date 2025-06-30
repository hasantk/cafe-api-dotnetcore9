using FluentValidation;
using KafeAPI.Application.Dtos.ReviewDtos;

namespace KafeAPI.Application.Validators.Review
{
    public class UpdateReviewValidator : AbstractValidator<UpdateReviewDto>
    {
        public UpdateReviewValidator()
        {
            RuleFor(x => x.Comment)
                .NotEmpty()
                .WithMessage("Yorum Boş Olamaz.")
                .Length(5, 500)
                .WithMessage("Yorum En Az 5 Karakter, En Fazla 500 Karakter Olamsi Gereklidir.");
            RuleFor(x => x.Rating)
                .NotEmpty()
                .WithMessage("Yıldız Boş Olamaz.")
                .InclusiveBetween(1, 5)
                .WithMessage("Yıldız Değeri 1 İle 5 Arasında Olmalıdır.");
        }
    }
}
