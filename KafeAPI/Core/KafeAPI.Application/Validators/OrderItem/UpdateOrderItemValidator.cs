using FluentValidation;
using KafeAPI.Application.Dtos.OrderItemDtos;

namespace KafeAPI.Application.Validators.OrderItem
{
    internal class UpdateOrderItemValidator : AbstractValidator<UpdateOrderItemDto>
    {
        public UpdateOrderItemValidator()
        {
            RuleFor(x => x.Quantity).
                NotEmpty().WithMessage("Sipariş Boş Olamaz.")
                .GreaterThan(0).WithMessage("Sipariş Adeti 0'dan Büyük Olmak Zorunda.");
        }
    }
}
