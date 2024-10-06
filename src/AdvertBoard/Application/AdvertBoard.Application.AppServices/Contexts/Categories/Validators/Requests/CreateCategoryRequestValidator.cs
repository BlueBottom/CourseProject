using AdvertBoard.Contracts.Contexts.Categories.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Categories.Validators.Requests;

/// <summary>
/// Валидатор запроса на создание категории.
/// </summary>
public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="CreateCategoryRequestValidator"/>.
    /// </summary>
    public CreateCategoryRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(25);
    }
}