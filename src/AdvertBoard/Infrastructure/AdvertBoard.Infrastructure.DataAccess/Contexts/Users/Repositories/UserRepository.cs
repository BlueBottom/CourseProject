﻿using System.Linq.Expressions;
using System.Text.Json;
using AdvertBoard.Application.AppServices.Contexts.Users.Models;
using AdvertBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertBoard.Application.AppServices.Exceptions;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using AdvertBoard.Contracts.Contexts.Users.Responses;
using AdvertBoard.Domain.Contexts.Users;
using AdvertBoard.Infrastructure.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AdvertBoard.Infrastructure.DataAccess.Contexts.Users.Repositories;

/// <summary>
/// Реполиторий для работы с пользователями.
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly IRepository<User> _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<UserRepository> _logger;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="repository">Глупый репозиторий.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="logger"></param>
    public UserRepository(IRepository<User> repository, IMapper mapper, ILogger<UserRepository> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<User?> FindUser(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _repository.GetAllFiltered(predicate).FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<UserWithPasswordModel?> FindByEmail(string email, CancellationToken cancellationToken)
    {
        return await _repository
            .GetAllFiltered(x => x.Email == email)
            .ProjectTo<UserWithPasswordModel>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public Task<bool> IsExistByPhone(string phone, CancellationToken cancellationToken)
    {
        return _repository.GetAll().AnyAsync(x => x.Phone == phone, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<bool> IsExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return _repository.GetAll().AnyAsync(x => x.Id == id , cancellationToken: cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Guid> AddAsync(User user, string password, CancellationToken cancellationToken)
    {
        user.Password = password;
        await _repository.AddAsync(user, cancellationToken);
        _logger.LogInformation($"Добавлен пользователь : {{User}}", JsonSerializer.Serialize(user));
        return user.Id;
    }

    /// <inheritdoc/>
    public async Task<UserResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(id, cancellationToken);
        if (user is null) throw new EntityNotFoundException("Пользователь не был найден.");

        return _mapper.Map<User, UserResponse>(user);
    }

    /// <inheritdoc/>
    public async Task<Guid> UpdateAsync(Guid userId, UpdateUserRequest updatedUser, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(userId, cancellationToken);

        if (user is null) throw new EntityNotFoundException("Пользователь не был найден.");
       
        _mapper.Map<UpdateUserRequest, User>(updatedUser, user);

        await _repository.UpdateAsync(user, cancellationToken);
        return user.Id;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(id, cancellationToken);
        if (user is null) throw new EntityNotFoundException("Пользователь не был найден.");
        await _repository.DeleteAsync(user, cancellationToken);

        return true;
    }

    /// <inheritdoc/>
    public async Task<PageResponse<ShortUserResponse>> GetByFilterWithPaginationAsync(
        ISpecification<User> specification,
        PaginationRequest paginationRequest, CancellationToken cancellationToken)
    {
        var result = new PageResponse<ShortUserResponse>();

        var query = _repository.GetAll();

        var elementsCount = await query.CountAsync(cancellationToken);
        result.TotalPages = result.TotalPages = (int)Math.Ceiling((double)elementsCount / paginationRequest.BatchSize);

        var paginationQuery = await query
            .Where(specification.PredicateExpression)
            .OrderBy(user => user.Id)
            .Skip(paginationRequest.BatchSize * (paginationRequest.PageNumber - 1))
            .Take(paginationRequest.BatchSize)
            .ProjectTo<ShortUserResponse>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        result.Response = paginationQuery;
        return result;
    }

    /// <inheritdoc/>
    public async Task UpdateRatingAsync(Guid id, decimal? rating, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(id, cancellationToken);
        if (user is null) throw new EntityNotFoundException("Пользователь не был найден.");

        var oldRating = user.Rating;
        user.Rating = rating;
        
        await _repository.UpdateAsync(user, cancellationToken);
        _logger.LogInformation($"Рейтинг пользователя изменен с {oldRating?.ToString() ?? "null"} на {rating}");
    }

    /// <inheritdoc/>
    public async Task ChangePassword(string email, string password, CancellationToken cancellationToken)
    {
        var userModel = await FindByEmail(email, cancellationToken);
        var user = await _repository.GetByIdAsync(userModel!.Id, cancellationToken);

        user!.Password = password;
        await _repository.UpdateAsync(user, cancellationToken);
        _logger.LogInformation($"Пароль пользователя {{UserModel}} был обновлен", userModel);
    }
}