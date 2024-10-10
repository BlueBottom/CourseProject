using AdvertBoard.Application.AppServices.Contexts.Categories.Repositories;
using AdvertBoard.Application.AppServices.Helpers;
using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Categories.Requests;
using AdvertBoard.Contracts.Contexts.Categories.Responses;
using AdvertBoard.Domain.Contexts.Categories;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Caching.Distributed;

namespace AdvertBoard.Application.AppServices.Contexts.Categories.Services;

/// <inheritdoc/>
public class CategoryService : ICategoryService
{
    private const string RedisKeyCategoryPrefix = "Category:";
    private const string ParentsCategoriesKey = "parent_categories";

    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly BusinessLogicAbstractValidator<CreateCategoryRequest> _createCategoryValidator;
    private readonly IDistributedCache _distributedCache;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="CategoryService"/>.
    /// </summary>
    /// <param name="categoryRepository">Репозиторий для работы с категориями.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="createCategoryValidator">Валидатор добавления категории.</param>
    /// <param name="distributedCache">Распределенный кеш.</param>
    public CategoryService(
        ICategoryRepository categoryRepository,
        IMapper mapper,
        BusinessLogicAbstractValidator<CreateCategoryRequest> createCategoryValidator,
        IDistributedCache distributedCache
    )
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _createCategoryValidator = createCategoryValidator;
        _distributedCache = distributedCache;
    }

    /// <inheritdoc/>
    public async Task<Guid> AddAsync(CreateCategoryRequest createCategoryRequest, CancellationToken cancellationToken)
    {
        await _createCategoryValidator.ValidateAndThrowAsync(createCategoryRequest, cancellationToken);

        var category = _mapper.Map<CreateCategoryRequest, Category>(createCategoryRequest);
        return await _categoryRepository.AddAsync(category, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<CategoryHierarchyResponse> GetHierarchyByIdAsync(Guid id, CancellationToken cancellationToken)
    {        
        var key = $"{RedisKeyCategoryPrefix}{id.ToString()}";
        var categoryFromCache = await _distributedCache.GetByKeyAsync<CategoryHierarchyResponse>(key, cancellationToken);
        if (categoryFromCache is not null) return categoryFromCache;

        var category = await _categoryRepository.GetHierarchyByIdAsync(id, cancellationToken);

        await _distributedCache.PutByKeyAsync(key, category, 1, cancellationToken);
        return category;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ShortCategoryResponse>> GetAllParentsAsync(CancellationToken cancellationToken)
    {
        var categoriesFromCache = await _distributedCache.GetByKeyAsync<IEnumerable<ShortCategoryResponse>>(
            ParentsCategoriesKey,
            cancellationToken);
        
        if (categoriesFromCache?.Any() ?? false) return categoriesFromCache;
        var categories = await _categoryRepository.GetAllParentsAsync(cancellationToken);

        await _distributedCache.PutByKeyAsync(ParentsCategoriesKey, categories, 1, cancellationToken);

        return categories;
    }

    /// <inheritdoc/>
    public async Task<ShortCategoryResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var key = $"{RedisKeyCategoryPrefix}{id.ToString()}";
        var categoryFromCache = await _distributedCache.GetByKeyAsync<ShortCategoryResponse>(key, cancellationToken);
        if (categoryFromCache is not null) return categoryFromCache;

        var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);

        await _distributedCache.PutByKeyAsync(key, category, 1, cancellationToken);
        return category;
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
        return _categoryRepository.UpdateAsync(id, updateCategoryRequest, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return _categoryRepository.DeleteAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> IsCategoryExists(Guid id, CancellationToken cancellationToken)
    {
        var key = $"{RedisKeyCategoryPrefix}{id.ToString()}";
        var categoryFromCache = await _distributedCache.GetByKeyAsync<CategoryHierarchyResponse>(key, cancellationToken);
        if (categoryFromCache is not null) return true;
        
        return await _categoryRepository.IsCategoryExists(id, cancellationToken);
    }
}