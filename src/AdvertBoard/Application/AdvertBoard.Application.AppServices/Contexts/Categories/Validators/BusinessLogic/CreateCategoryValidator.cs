using AdvertBoard.Application.AppServices.Contexts.Categories.Repositories;
using AdvertBoard.Application.AppServices.Services;
using AdvertBoard.Contracts.Contexts.Categories.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Categories.Validators.BusinessLogic;

/// <summary>
/// Валидатор, проверяющий правила бизнес логики при давлении категории.
/// </summary>
public class CreateCategoryValidator : BusinessLogicAbstractValidator<CreateCategoryRequest>
{
    private readonly ICategoryRepository _categoryRepository;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="CreateCategoryValidator"/>.
    /// </summary>
    /// <param name="categoryRepository">Умный репозиторий для работы с категориями.</param>
    public CreateCategoryValidator(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;

        RuleFor(x => x.ParentId)
            .MustAsync(IsCategoryExists)
            .When(x => x.ParentId.HasValue)
            .WithMessage("Родительская атегория не найдена в БД.");
    }
    
    private Task<bool> IsCategoryExists(Guid? categoryId, CancellationToken cancellationToken)
    {
        return _categoryRepository.IsCategoryExists(categoryId!.Value, cancellationToken);
    }
}