using FluentValidation;
using KafeAPI.Application.Dtos.CategoryDtos;

namespace KafeAPI.Application.Validators.Category
{
    public class AddCategoryValidator : AbstractValidator<CreateCategoryDto>
    {
        public AddCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori Adı Boş Olamaz.")
                .Length(3, 30).WithMessage("Kategori Adı 3 ile 30 Karakter Arasında Olmalıdır.");
        }
    }
}
