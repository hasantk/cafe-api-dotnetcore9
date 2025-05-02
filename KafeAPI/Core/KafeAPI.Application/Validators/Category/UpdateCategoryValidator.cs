using FluentValidation;
using KafeAPI.Application.Dtos.CategoryDtos;

namespace KafeAPI.Application.Validators.Category
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori Adı Boş Olamaz.")
                .Length(3, 30).WithMessage("Kategori Adı 3 ile 30 Karakter Araında Olmalıdır.");
        }
    }
}
