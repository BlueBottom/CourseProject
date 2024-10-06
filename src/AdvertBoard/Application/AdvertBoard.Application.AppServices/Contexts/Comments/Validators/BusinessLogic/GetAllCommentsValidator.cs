using AdvertBoard.Application.AppServices.Contexts.Adverts.Repositories;
using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Comments.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Comments.Validators.BusinessLogic;

/// <summary>
/// Валидатор, проверяющий правила бизнес логики для получения комментариев к объявлению.
/// </summary>
public class GetAllCommentsValidator : BusinessLogicAbstractValidator<GetAllCommentsRequest>
{
    private readonly IAdvertRepository _advertRepository;
    
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="GetAllCommentsValidator"/>.
    /// </summary>
    /// <param name="advertRepository">Умный репозиторий для работы с объявлениями.</param>
    public GetAllCommentsValidator(IAdvertRepository advertRepository)
    {
        _advertRepository = advertRepository;

        RuleFor(x => x.AdvertId)
            .MustAsync(IsAdvertExists)
            .WithMessage("Объявление не найдено.");
    }
    
    private Task<bool> IsAdvertExists(Guid? id, CancellationToken cancellationToken)
    {
        return _advertRepository.IsAdvertExists(id!.Value, cancellationToken);
    }
}