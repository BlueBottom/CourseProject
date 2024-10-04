using AdvertBoard.Application.AppServices.Contexts.Categories.Services;
using AdvertBoard.Application.AppServices.Services;
using AdvertBoard.Contracts.Contexts.Adverts.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Validators.BusinessLogic;

/// <summary>
/// Валидатор, проверяющий правила бизнес логики при создании объявления.
/// </summary>
public class CreateAdvertValidator : BusinessLogicAbstractValidator<CreateAdvertRequest>
{
    private readonly ICategoryService _categoryService;
    
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="CreateAdvertValidator"/>
    /// </summary>
    /// <param name="categoryService">Сервис для работы с категориями.</param>
    public CreateAdvertValidator(ICategoryService categoryService)
    {
        _categoryService = categoryService;

        RuleFor(x => x.CategoryId)
            .MustAsync(IsCategoryExists)
            .WithMessage("Категория не найдена в БД.");
    }

    private Task<bool> IsCategoryExists(Guid? categoryId, CancellationToken cancellationToken)
    {
        return _categoryService.IsCategoryExists(categoryId!.Value, cancellationToken);
    }
}