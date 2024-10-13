using FluentValidation;

namespace AdvertBoard.Application.AppServices.Validators
{
    /// <summary>
    /// Класс, содержащий расширения для FluentValidation для реализации кастомной валидации.
    /// </summary>
    public static class CustomValidators
    {
        /// <summary>
        /// Добавляет правило валидации номера телефона, соответствующего российскому формату.
        /// </summary>
        /// <typeparam name="T">Тип модели, для которой применяется правило.</typeparam>
        /// <param name="ruleBuilder">Строитель правил валидации.</param>
        /// <returns>Объект, позволяющий продолжить построение правил.</returns>
        public static IRuleBuilderOptions<T, string?> MatchPhoneNumberRule<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            var regex = @"^(\+7|8)\s?\(?\d{3}\)?\s?\d{3}[-\s]?\d{2}[-\s]?\d{2}$";

            return ruleBuilder
                .Matches(regex)
                .WithMessage("Неверный формат номера телефона.");
        }

        /// <summary>
        /// Добавляет правила валидации пароля.
        /// </summary>
        /// <typeparam name="T">Тип модели, для которой применяется правило.</typeparam>
        /// <param name="ruleBuilder">Строитель правил валидации.</param>
        /// <returns>Объект, позволяющий продолжить построение правил.</returns>
        public static IRuleBuilderOptions<T, string?> PasswordRule<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .MinimumLength(8).WithMessage("Пароль должен быть не менее 8 символов.")
                .Matches("[A-Z]").WithMessage("Пароль должен содерать не менее 1 литеры верхнего регистра.")
                .Matches("[a-z]").WithMessage("Пароль должен содерать не менее 1 литеры нижнего регистра.")
                .Matches("[0-9]").WithMessage("Пароль должен содерать не менее 1 цифры.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Пароль должен содержать не менее 1 специального символа.");
        }

        /// <summary>
        /// Добавляет правило валидации рейтинга типа <see cref="int"/>.
        /// </summary>
        /// <typeparam name="T">Тип модели, для которой применяется правило.</typeparam>
        /// <param name="ruleBuilder">Строитель правил валидации.</param>
        /// <returns>Объект, позволяющий продолжить построение правил.</returns>
        public static IRuleBuilderOptions<T, int?> RatingRule<T>(this IRuleBuilder<T, int?> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(5);
        }
        
        /// <summary>
        /// Добавляет правило валидации рейтинга типа <see cref="decimal"/>.
        /// </summary>
        /// <typeparam name="T">Тип модели, для которой применяется правило.</typeparam>
        /// <param name="ruleBuilder">Строитель правил валидации.</param>
        /// <returns>Объект, позволяющий продолжить построение правил.</returns>
        public static IRuleBuilderOptions<T, decimal?> RatingRule<T>(this IRuleBuilder<T, decimal?> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(5);
        }

        /// <summary>
        /// Добавляет правило валидации email-адреса.
        /// </summary>
        /// <typeparam name="T">Тип модели, для которой применяется правило.</typeparam>
        /// <param name="ruleBuilder">Строитель правил валидации.</param>
        /// <returns>Объект, позволяющий продолжить построение правил.</returns>
        public static IRuleBuilderOptions<T, string?> EmailRule<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            const String pattern =
                @"^([0-9a-zA-Z]" + // Start with a digit or alphabetical
                @"([\+\-_\.][0-9a-zA-Z]+)*" + // No continuous or ending +-_. chars in email
                @")+" +
                @"@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,17})$";
            
            return ruleBuilder
                .NotEmpty().WithMessage("Email не должен быть пустым.")
                .MaximumLength(50).WithMessage("Email не должен превышать 50 символов.")
                .Matches(pattern)
                .WithMessage("Неверный формат email.");
        }

        /// <summary>
        /// Добавляет правило валидации имени пользователя.
        /// </summary>
        /// <typeparam name="T">Тип модели, для которой применяется правило.</typeparam>
        /// <param name="ruleBuilder">Строитель правил валидации.</param>
        /// <returns>Объект, позволяющий продолжить построение правил.</returns>
        public static IRuleBuilderOptions<T, string?> NameRule<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .Matches(@"^[a-zA-Zа-яА-ЯёЁ]{3,15}$")
                .WithMessage("Имя должно содержать только буквы и быть длиной от 3 до 15 символов.");
        }  
        
        /// <summary>
        /// Добавляет правило валидации фамилии пользователя.
        /// </summary>
        /// <typeparam name="T">Тип модели, для которой применяется правило.</typeparam>
        /// <param name="ruleBuilder">Строитель правил валидации.</param>
        /// <returns>Объект, позволяющий продолжить построение правил.</returns>
        public static IRuleBuilderOptions<T, string?> LastnameRule<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .Matches(@"^[a-zA-Zа-яА-ЯёЁ]{0,25}$")
                .WithMessage("Фамилия должна содержать только буквы и быть длиной от 3 до 25 символов.");
        } 
    }
}
