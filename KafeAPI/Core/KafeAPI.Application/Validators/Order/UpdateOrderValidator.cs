using FluentValidation;
using KafeAPI.Application.Dtos.OrderDtos;

namespace KafeAPI.Application.Validators.Order
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderDto>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.TotalPrice)
               .NotEmpty().WithMessage("Sipariş Ücreti Boş Olamaz.")
               .GreaterThan(0).WithMessage("Sipariş Ücreti 0'dan Büyük Olmalıdır.");
        }
    }
}
