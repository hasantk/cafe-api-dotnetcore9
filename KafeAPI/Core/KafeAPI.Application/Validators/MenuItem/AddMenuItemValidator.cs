using FluentValidation;
using KafeAPI.Application.Dtos.MenuItemDtos;

namespace KafeAPI.Application.Validators.MenuItem
{
    public class AddMenuItemValidator : AbstractValidator<CreateMenuItemDto>
    {
        public AddMenuItemValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Menu Item Adı Boş Olamaz.")
                .Length(2, 40).WithMessage("Menu Item Adı 2 İle 40 Karakter Arasında Olmak Zorundadır.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Menu Item Açıklaması Boş Olamaz.")
                .Length(5, 100).WithMessage("Menu Item Açıklaması 5 İle 100 Karakter Arasında Olmak Zorundadır.");
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Menu Item Fiyatı Boş Olamaz.")
                .GreaterThan(0).WithMessage("Menu Item Fiyatı 0'dan Büyük Olmak Zorundadır.");
            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Menu Item Fotoğraf Url'i Boş Olamaz.");
        }
    }
}
