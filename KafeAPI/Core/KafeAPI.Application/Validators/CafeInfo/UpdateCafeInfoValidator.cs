using FluentValidation;
using KafeAPI.Application.Dtos.CafeInfoDtos;

namespace KafeAPI.Application.Validators.CafeInfo
{
    public class UpdateCafeInfoValidator : AbstractValidator<UpdateCafeInfoDto>
    {
        public UpdateCafeInfoValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Kafe Adı Boş Olamaz.")
               .MaximumLength(100).WithMessage("Kafe Adi En Fazla 100 Karakter Olabilir.");
            RuleFor(x => x.Adress)
                .NotEmpty().WithMessage("Kafe Adresi Boş Olamaz.")
                .MaximumLength(500).WithMessage("Kafe Adresi En Fazla 500 Karakter Olmalı.");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Kafe Telefon Numarası Boş Olamaz.")
                .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Kafe Telefon Numarası Geçerli Bir Formatta Olmalı.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Kafe E-posta Adresi Boş Olamaz.")
                .EmailAddress().WithMessage("Geçerli Bir E-Posta Adresi Giriniz.");
        }
    }
}
