using AdvertBoard.Contracts.Contexts.Comments.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Comments.Validators.Requests;

/// <summary>
/// Валидатор запроса на обновление комментария.
/// </summary>
public class UpdateCommentRequestValidator : AbstractValidator<UpdateCommentRequest>
{
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="UpdateCommentRequestValidator"/>.
    /// </summary>
    public UpdateCommentRequestValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(400);
    }
}