using FluentValidation;

namespace ViewModel.Accounting.ViewModels.Validator
{
    public class CountryValidator : AbstractValidator<CountryViewModel>
    {
        public CountryValidator()
        {
            RuleFor(x => x.CountryCode)
                .NotEmpty().NotNull()
                .WithMessage("کد کشور را وارد کنید")
                .WithErrorCode("10");

            RuleFor(x => x.CountryCode).MaximumLength(5)
                .WithMessage("حداکثر طول کد کشور 5 کاراکتر میباشد")
                .WithErrorCode("02");
        }
    }
}
