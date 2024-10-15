using AdvertBoard.Contracts.Contexts.Categories.Requests;
using AdvertBoard.Contracts.Contexts.Categories.Responses;
using AdvertBoard.Domain.Contexts.Categories;

namespace AdvertBoard.Application.AppServices.Contexts.Categories.Repositories;

/// <summary>
/// Репозиторий для работы с категориями.
/// </summary>
public interface ICategoryRepository
{
    /// <summary>
    /// Добавляет категорию.
    /// </summary>
    /// <param name="category">Сущность категории.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    Task<Guid> AddAsync(Category category, CancellationToken cancellationToken);

    /// <summary>
    /// Получает категорию и ее подкатегории.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию категорий.</returns>
    Task<CategoryHierarchyResponse> GetHierarchyByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает полный список категорий.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию категорий.</returns>
    Task<IReadOnlyCollection<ShortCategoryResponse>> GetAllParentsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Получает категорию.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Укороченную сущность категории.</returns>
    Task<ShortCategoryResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет категорию.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="updatedCategory">Обновленная сущность.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    Task<Guid> UpdateAsync(Guid id, UpdateCategoryRequest updatedCategory, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удаляет категорию.
    /// </summary>
    /// <param name="id">Идентификтаор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статус действия типа <see cref="bool"/>.</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Строит иерархию категорий для каждого идентификаторо коллекции..
    /// </summary>
    /// <param name="ids">Коллекция идентификаторов.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию всех дочерних идентификаторов.</returns>
    Task<IEnumerable<Guid>> GetHierarchyIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

    /// <summary>
    /// Проверяет наличие категории в БД.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Наличие категории в БД.</returns>
    Task<bool> IsCategoryExists(Guid id, CancellationToken cancellationToken);
}