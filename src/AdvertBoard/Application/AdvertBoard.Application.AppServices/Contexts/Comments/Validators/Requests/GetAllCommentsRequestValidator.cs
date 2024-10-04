using AdvertBoard.Application.AppServices.Services;
using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Comments.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Comments.Validators.Requests;

/// <summary>
/// Валидатор запроса на получение комментариев определенного объявления.
/// </summary>
public class GetAllCommentsRequestValidator : AbstractValidator<GetAllCommentsRequest>
{
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="GetAllCommentsRequestValidator"/>.
    /// </summary>
    public GetAllCommentsRequestValidator(IValidator<PaginationRequest> paginationValidator)
    {
        Include(paginationValidator);
        RuleFor(x => x.AdvertId)
            .NotEmpty();
    }
}