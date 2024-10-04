using AdvertBoard.Application.AppServices.Contexts.Adverts.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Comments.Repositories;
using AdvertBoard.Application.AppServices.Services;
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
            .MustAsync(IsAdvertExists)
            .WithMessage("Объявление не найдено.");

        RuleFor(x => x.ParentId)
            .MustAsync(IsParentCommentExists)
            .WithMessage("Родительский комментарий не найден.")
            .When(x => x.ParentId.HasValue);
    }

    private Task<bool> IsAdvertExists(Guid? id, CancellationToken cancellationToken)
    {
        return _advertRepository.IsAdvertExists(id!.Value, cancellationToken);
    }

    private Task<bool> IsParentCommentExists(Guid? id, CancellationToken cancellationToken)
    {
        return _commentRepository.IsCommentExists(id!.Value, cancellationToken);
    }
}