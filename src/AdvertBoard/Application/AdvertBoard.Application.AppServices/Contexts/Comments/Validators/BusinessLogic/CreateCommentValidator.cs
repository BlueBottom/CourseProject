using AdvertBoard.Application.AppServices.Contexts.Adverts.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Comments.Repositories;
using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Comments.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Comments.Validators.BusinessLogic;

/// <summary>
/// Валидатор, проверяющий правила бизнес логики при создании комментария.
/// </summary>
public class CreateCommentValidator : BusinessLogicAbstractValidator<CreateCommentRequest>
{
    private readonly IAdvertRepository _advertRepository;
    private readonly ICommentRepository _commentRepository;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="CreateCommentValidator"/>.
    /// </summary>
    /// <param name="advertRepository">Умный репозиторий для работы с объявлениями.</param>
    /// <param name="commentRepository">Умный репозиторий для работы с комментариями.</param>
    public CreateCommentValidator(IAdvertRepository advertRepository, ICommentRepository commentRepository)
    {
        _advertRepository = advertRepository;
        _commentRepository = commentRepository;

        RuleFor(x => x.AdvertId)
            .NotEmpty()
            .MustAsync(IsAdvertExistsAndActive)
            .WithMessage("Объявление не найдено.");

        RuleFor(x => x.ParentId)
            .MustAsync(IsParentCommentExists)
            .WithMessage("Родительский комментарий не найден.")
            .When(x => x.ParentId.HasValue);

        RuleFor(x => new { x.ParentId, x.AdvertId })
            .MustAsync((args, token) => IsCurrentCommentRelatedToCurrentAdvert(args.ParentId!, args.AdvertId!, token))
            .When(x => x.ParentId.HasValue)
            .WithMessage("В этом объявлении не существует комментария с таким id.");
    }

    private Task<bool> IsAdvertExistsAndActive(Guid? id, CancellationToken cancellationToken)
    {
        return _advertRepository.IsAdvertExistsAndActive(id!.Value, cancellationToken);
    }

    private Task<bool> IsParentCommentExists(Guid? id, CancellationToken cancellationToken)
    {
        return _commentRepository.IsCommentExists(id!.Value, cancellationToken);
    }

    private Task<bool> IsCurrentCommentRelatedToCurrentAdvert(Guid? parentId, Guid? advertId,
        CancellationToken cancellationToken)
    {
        return _commentRepository.IsCurrentCommentRelatedToCurrentAdvert(parentId, advertId, cancellationToken);
    }
}