using AdvertBoard.Contracts.Common;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Validators;

/// <summary>
/// Валидатор модели запроса для пагинации.
/// </summary>
public class PaginationValidator : AbstractValidator<PaginationRequest>
{
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="PaginationValidator"/>.
    /// </summary>
    public PaginationValidator()
    {
        RuleFor(x => x.PageNumber)
            .NotEmpty()
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.BatchSize)
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
    }
}