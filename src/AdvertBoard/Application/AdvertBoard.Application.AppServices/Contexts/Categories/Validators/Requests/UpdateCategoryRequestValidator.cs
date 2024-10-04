using AdvertBoard.Contracts.Contexts.Categories.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Categories.Validators.Requests;

/// <summary>
/// Валидатор запроса на обновление категории.
/// </summary>
public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
{
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="CreateCategoryRequestValidator"/>.
    /// </summary>
    public UpdateCategoryRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(25);
    }
}