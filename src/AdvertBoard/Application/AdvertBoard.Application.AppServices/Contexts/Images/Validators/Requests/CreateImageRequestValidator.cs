using AdvertBoard.Contracts.Contexts.Images.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Images.Validators.Requests;

/// <summary>
/// Валидатор модели запроса на добавление изображения.
/// </summary>
public class CreateImageRequestValidator : AbstractValidator<CreateImageRequest>
{
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="CreateImageRequestValidator"/>.
    /// </summary>
    public CreateImageRequestValidator()
    {
        RuleFor(x => x.AdvertId)
            .NotEmpty();

        RuleFor(x => x.File)
            .NotEmpty();
    }
}