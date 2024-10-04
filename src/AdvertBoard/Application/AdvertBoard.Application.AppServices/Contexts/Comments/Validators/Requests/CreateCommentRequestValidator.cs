using AdvertBoard.Contracts.Contexts.Comments.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Comments.Validators.Requests;

/// <summary>
/// Валидатор запроса на создание комменатрия.
/// </summary>
public class CreateCommentRequestValidator : AbstractValidator<CreateCommentRequest>
{
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="CreateCommentRequestValidator"/>.
    /// </summary>
    public CreateCommentRequestValidator()
    {
        RuleFor(x => x.AdvertId)
            .NotEmpty();

        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(400);
    }
}