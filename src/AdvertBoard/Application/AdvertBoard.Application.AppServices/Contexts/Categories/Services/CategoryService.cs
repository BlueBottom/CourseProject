using System.Collections;
using AdvertBoard.Application.AppServices.Contexts.Categories.Repositories;
using AdvertBoard.Contracts.Contexts.Categories;
using AdvertBoard.Domain.Contexts.Categories;
using AutoMapper;

namespace AdvertBoard.Application.AppServices.Contexts.Categories.Services;

/// <inheritdoc/>
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="CategoryService"/>.
    /// </summary>
    /// <param name="categoryRepository">Репозиторий для работы с категориями.</param>
    /// <param name="mapper">Маппер.</param>
    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public Task<Guid> AddAsync(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<CreateCategoryDto, Category>(createCategoryDto);
        return _categoryRepository.AddAsync(category, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<CategoryHierarchyDto> GetHierarchyByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _categoryRepository.GetHierarchyByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<ShortCategoryDto>> GetAllParentsAsync(CancellationToken cancellationToken)
    {
        return _categoryRepository.GetAllParentsAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public Task<ShortCategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _categoryRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<Guid>> GetHierarchyIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        return _categoryRepository.GetHierarchyIdsAsync(ids, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<Guid> UpdateAsync(Guid id, UpdateCategoryDto updateCategoryDto, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<UpdateCategoryDto, Category>(updateCategoryDto);
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