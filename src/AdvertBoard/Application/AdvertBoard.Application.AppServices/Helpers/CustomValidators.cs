using AdvertBoard.Application.AppServices.Contexts.Categories.Services;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Helpers
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
    }
}