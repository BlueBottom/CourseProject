using AdvertBoard.Application.AppServices.Contexts.Categories.Repositories;
using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Categories.Requests;
using AdvertBoard.Contracts.Contexts.Categories.Responses;
using AdvertBoard.Domain.Contexts.Categories;
using AutoMapper;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Categories.Services;

/// <inheritdoc/>
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly BusinessLogicAbstractValidator<CreateCategoryRequest> _createCategoryValidator;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="CategoryService"/>.
    /// </summary>
    /// <param name="categoryRepository">Репозиторий для работы с категориями.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="createCategoryValidator">Валидатор добавления категории.</param>
    public CategoryService(
        ICategoryRepository categoryRepository, 
        IMapper mapper,
        BusinessLogicAbstractValidator<CreateCategoryRequest> createCategoryValidator)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _createCategoryValidator = createCategoryValidator;
    }

    /// <inheritdoc/>
    public async Task<Guid> AddAsync(CreateCategoryRequest createCategoryRequest, CancellationToken cancellationToken)
    {
        await _createCategoryValidator.ValidateAndThrowAsync(createCategoryRequest, cancellationToken);
        
        var category = _mapper.Map<CreateCategoryRequest, Category>(createCategoryRequest);
        return await _categoryRepository.AddAsync(category, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<CategoryHierarchyResponse> GetHierarchyByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _categoryRepository.GetHierarchyByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<ShortCategoryResponse>> GetAllParentsAsync(CancellationToken cancellationToken)
    {
        return _categoryRepository.GetAllParentsAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public Task<ShortCategoryResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _categoryRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<Guid>> GetHierarchyIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        return _categoryRepository.GetHierarchyIdsAsync(ids, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<Guid> UpdateAsync(Guid id, UpdateCategoryRequest updateCategoryRequest,
        CancellationToken cancellationToken)
    {
        var category = _mapper.Map<UpdateCategoryRequest, Category>(updateCategoryRequest);
        return _categoryRepository.UpdateAsync(id, category, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return _categoryRepository.DeleteAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<bool> IsCategoryExists(Guid id, CancellationToken cancellationToken)
    {
        return _categoryRepository.IsCategoryExists(id, cancellationToken);
    }
}