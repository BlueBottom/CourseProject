using FluentValidation;

namespace AdvertBoard.Application.AppServices.Validators
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, string?> MatchPhoneNumberRule<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            var regex = @"^(\+7|8)\s?\(?\d{3}\)?\s?\d{3}[-\s]?\d{2}[-\s]?\d{2}$";

            return ruleBuilder
                .Matches(regex)
                .WithMessage("Неверный формат номера телефона.");
        }

        public static IRuleBuilderOptions<T, string> PasswordRule<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .MinimumLength(8).WithMessage("Пароль должен быть не менее 8 символов.")
                .Matches("[A-Z]").WithMessage("Пароль должен содерать не менее 1 литеры верхнего регистра.")
                .Matches("[a-z]").WithMessage("Пароль должен содерать не менее 1 литеры нижнего регистра..")
                .Matches("[0-9]").WithMessage("Пароль должен содерать не менее 1 цифры.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Пароль должен содержать не менее 1 специального символа.");
        }

        public static IRuleBuilderOptions<T, int?> RatingRule<T>(this IRuleBuilder<T, int?> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(5);
        }
        
        public static IRuleBuilderOptions<T, decimal?> RatingRule<T>(this IRuleBuilder<T, decimal?> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(5);
        }
    }
}