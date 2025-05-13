using FluentValidation;
using KafeAPI.Application.Dtos.OrderDtos;

namespace KafeAPI.Application.Validators.Order
{
    public class AddOrderValidator : AbstractValidator<CreateOrderDto>
    {
        public AddOrderValidator()
        {
            RuleFor(x => x.TotalPrice)
                .NotEmpty().WithMessage("Sipariş Ücreti Boş Olamaz.")
                .GreaterThan(0).WithMessage("Sipariş Ücreti 0'dan Büyük Olmalıdır.");
        }
    }
}
