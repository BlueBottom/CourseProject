using AdvertBoard.Application.AppServices.Contexts.Categories.Repositories;
using AdvertBoard.Application.AppServices.Exceptions;
using AdvertBoard.Contracts.Contexts.Categories.Requests;
using AdvertBoard.Contracts.Contexts.Categories.Responses;
using AdvertBoard.Domain.Contexts.Categories;
using AdvertBoard.Infrastructure.DataAccess.Helpers;
using AdvertBoard.Infrastructure.Repository.Relational;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AdvertBoard.Infrastructure.DataAccess.Contexts.Categories.Repositories;

/// <inheritdoc/>
public class CategoryRepository : ICategoryRepository
{
    private const string GetHierarchyByIdSqlQuery = "Contexts.Categories.Resources.GetHierarchyByIdSqlQuery.txt";
    private const string GetHierarchyIdsSqlQuery = "Contexts.Categories.Resources.GetHierarchyIdsSqlQuery.txt";
    
    private readonly IRelationalRepository<Category> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="CategoryRepository"/>.
    /// </summary>
    /// <param name="repository">Глупый репозиторий.</param>
    /// <param name="mapper">Маппер.</param>
    public CategoryRepository(IRelationalRepository<Category> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<Guid> AddAsync(Category category, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(category, cancellationToken);
        return category.Id;
    }

    /// <inheritdoc/>
    public async Task<CategoryHierarchyResponse> GetHierarchyByIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        string sql = await EmbeddedResourceHelper.GetEmbeddedResourceAsString(GetHierarchyByIdSqlQuery, cancellationToken);
       
        var query = await _repository
            .GetBySql(sql, id)
            .AsNoTrackingWithIdentityResolution()
            .ToArrayAsync(cancellationToken);

        if (query.Length == 0) throw new EntityNotFoundException("Категория не была найдена.");
        
        return _mapper.Map<CategoryHierarchyResponse>(query.FirstOrDefault());
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<ShortCategoryResponse>> GetAllParentsAsync(
        CancellationToken cancellationToken)
    {
        return await _repository
            .GetAll()
            .Where(x => x.ParentId == null)
            .ProjectTo<ShortCategoryResponse>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<ShortCategoryResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(id, cancellationToken);
        if (category is null) throw new EntityNotFoundException("Категория не была найдена.");
        
        return _mapper.Map<Category, ShortCategoryResponse>(category);
    }

    /// <inheritdoc/>
    public async Task<Guid> UpdateAsync(Guid id, UpdateCategoryRequest updatedCategory,
        CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(id, cancellationToken);
        if (category is null) throw new EntityNotFoundException("Категория не была найдена.");
        _mapper.Map(updatedCategory, category);
        await _repository.UpdateAsync(category, cancellationToken);
        
        return category.Id;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(id, cancellationToken);
        if (category is null) throw new EntityNotFoundException("Категория не была найдена.");
        await _repository.DeleteAsync(category, cancellationToken);
        
        return true;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Guid>> GetHierarchyIdsAsync(IEnumerable<Guid> ids,
        CancellationToken cancellationToken)
    {
        var placeholders = string.Join(",", Enumerable.Range(0, ids.Count())
            .Select(i => "{" + i + "}"));
        
        var values = ids.Cast<object>().ToArray();
        
        string template = await EmbeddedResourceHelper.GetEmbeddedResourceAsString(GetHierarchyIdsSqlQuery, cancellationToken);
        var sql = string.Format(template, placeholders);
        
        var query = await _repository
            .GetBySql(sql, values)
            .Select(c => c.Id)
            .ToArrayAsync(cancellationToken);
        
        if (query.Length == 0) throw new EntityNotFoundException("Категория не была найдена.");
        
        return query;
    }

    /// <inheritdoc/>
    public Task<bool> IsCategoryExists(Guid id, CancellationToken cancellationToken)
    {
        return _repository.GetAll().AnyAsync(x => x.Id == id, cancellationToken);
    }
}