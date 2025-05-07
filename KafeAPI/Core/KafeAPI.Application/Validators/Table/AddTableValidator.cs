using FluentValidation;
using KafeAPI.Application.Dtos.TableDtos;

namespace KafeAPI.Application.Validators.Table
{
    public class AddTableValidator : AbstractValidator<CreateTableDto>
    {
        public AddTableValidator()
        {
            RuleFor(x => x.TableNumber)
                .NotEmpty()
                .WithMessage("Masa Numarası Boş Olamaz.")
                .GreaterThan(0)
                .WithMessage("Masa Numarası 0'dan Büyük Olmalıdır.");
            RuleFor(x => x.Capacity)
                .NotEmpty()
                .WithMessage("Kapasite Boş Olamaz.")
                .GreaterThan(0)
                .WithMessage("Kapasite 0'dan Büyük Olmalıdır.");
        }
    }
}
