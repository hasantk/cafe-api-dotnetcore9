using FluentValidation;
using KafeAPI.Application.Dtos.UserDtos;

namespace KafeAPI.Application.Validators.User
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Ad Alanı Boş Olamaz.")
                .MinimumLength(2)
                .WithMessage("Ad Alanı En Az 2 Karakter Olmalıdır.");
            RuleFor(x => x.Surname)
                .NotEmpty()
                .WithMessage("Soyad Alanı Boş Olamaz.")
                .MinimumLength(2)
                .WithMessage("Soyad Alanı En Az 2 Karakter Olmalıdır.");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email Alanı Boş Olamaz.")
                .MinimumLength(2)
                .WithMessage("Geçersiz Email Adresi.");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Şifre Alanı Boş Olamaz.")
                .MinimumLength(6)
                .WithMessage("Şifre Alanı En Az 6 Karakter Olmalıdır.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$")
                .WithMessage("Şifre En Az Bir Büyük Harf, Bir Küçük Harf Ve Bir Rakam İçermelidir.");

        }
    }
}
