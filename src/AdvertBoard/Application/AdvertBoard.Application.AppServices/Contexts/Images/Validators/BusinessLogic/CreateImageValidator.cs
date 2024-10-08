using AdvertBoard.Application.AppServices.Contexts.Adverts.Repositories;
using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Images.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Images.Validators.BusinessLogic;

/// <summary>
/// Валидатор, проверяющий правила бизнес логики при добавлении изображения.
/// </summary>
public class CreateImageValidator : BusinessLogicAbstractValidator<CreateImageRequest>
{
    private readonly IAdvertRepository _advertRepository;
    /// <summary>
    /// Инициализирует экземпляр <see cref="CreateImageValidator"/>.
    /// </summary>
    public CreateImageValidator(IAdvertRepository advertRepository)
    {
        _advertRepository = advertRepository;

        RuleFor(x => x.AdvertId)
            .MustAsync(IsAdvertExist)
            .WithMessage("Объявление не найдено.");
    }

    private Task<bool> IsAdvertExist(Guid id, CancellationToken cancellationToken)
    {
        return _advertRepository.IsAdvertExistsAndActive(id, cancellationToken);
    }
}