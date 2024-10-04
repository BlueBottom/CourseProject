using System.Collections;
using AdvertBoard.Contracts.Contexts.Categories;

namespace AdvertBoard.Application.AppServices.Contexts.Categories.Services;

/// <summary>
/// Сервис для работы с категориями.
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// Добавляет категорию.
    /// </summary>
    /// <param name="createCategoryDto">Модель запроса на создание.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    Task<Guid> AddAsync(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken);

    /// <summary>
    /// Получает категорию и ее подкатегории.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Коллекцию категорий.</returns>
    Task<CategoryHierarchyDto> GetHierarchyByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает все доступные категории с подкатегориями.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию категорий.</returns>
    Task<IEnumerable<ShortCategoryDto>> GetAllParentsAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Получает категорию.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Укороченную сущность категории.</returns>
    Task<ShortCategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Строит иерархию категорий для каждого идентификаторо коллекции..
    /// </summary>
    /// <param name="ids">Коллекция идентификаторов.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию всех дочерних идентификаторов.</returns>
    Task<IEnumerable<Guid>> GetHierarchyIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновляет информацию о категории.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="updateCategoryDto">Модель запроса для обновления категории.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    Task<Guid> UpdateAsync(Guid id, UpdateCategoryDto updateCategoryDto, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет категорию.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статус действия типа <see cref="bool"/>.</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Проверяет наличие категории в БД.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Наличие категории в БД.</returns>
    Task<bool> IsCategoryExists(Guid id, CancellationToken cancellationToken);
}