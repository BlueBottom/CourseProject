﻿using System.Linq.Expressions;
using AdvertBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Contracts.Contexts.Users;
using AdvertBoard.Contracts.Shared;
using AdvertBoard.Domain.Contexts.Users;
using AdvertBoard.Infrastructure.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AdvertBoard.Infrastructure.DataAccess.Contexts.Users.Repositories;

/// <summary>
/// Реполиторий для работы с пользователями.
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly IRepository<User> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="repository">Глупый репозиторий.</param>
    /// <param name="mapper">Маппер.</param>
    public UserRepository(IRepository<User> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<User?> FindUser(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _repository.GetAllFiltered(predicate).FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Guid> AddUser(User user, string password, CancellationToken cancellationToken)
    {
        user.Password = password;
        await _repository.AddAsync(user, cancellationToken);
        return user.Id;
    }

    /// <inheritdoc/>
    public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(id, cancellationToken);
        //TODO: нормальное исключение
        if (user is null) throw new Exception();
        return _mapper.Map<User, UserDto>(user);
    }

    /// <inheritdoc/>
    public async Task<Guid> UpdateAsync(User updatedUser, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(updatedUser.Id, cancellationToken);
        // TODO: исключение.
        if (user is null) throw new Exception();
        var checkUser = await FindUser(x => x.Email == updatedUser.Email, cancellationToken);
        //TODO: исключение - пользователь с таким email уже существует.
        if (checkUser is not null && checkUser.Id != updatedUser.Id) throw new Exception();
        checkUser = await FindUser(x => x.Phone == updatedUser.Phone, cancellationToken);
        //TODO: исключение - пользователь с таким телефоном уже существует.
        if (checkUser is not null && checkUser.Id != updatedUser.Id) throw new Exception();
        _mapper.Map<User, User>(updatedUser, user);
        
        await _repository.UpdateAsync(user, cancellationToken);
        return user.Id;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(id, cancellationToken);
        //TODO: нормальное исключение.
        if (user is null) throw new Exception();
        await _repository.DeleteAsync(user, cancellationToken);
        return true;
    }

    /// <inheritdoc/>
    public async Task<PageResponse<ShortUserDto>> GetAllAsync(ISpecification<User> specification,
        PaginationRequest paginationRequest, CancellationToken cancellationToken)
    {
        var result = new PageResponse<ShortUserDto>();
        
        var query = _repository.GetAll();
        
        var elementsCount = await query.CountAsync(cancellationToken);
        result.TotalPages = result.TotalPages = (int)Math.Ceiling((double)elementsCount / paginationRequest.BatchSize);
        
        var paginationQuery = await query
            .Where(specification.PredicateExpression)
            .OrderBy(user => user.Id)
            .Skip(paginationRequest.BatchSize * (paginationRequest.PageNumber - 1))
            .Take(paginationRequest.BatchSize)
            .ProjectTo<ShortUserDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
        
        result.Response = paginationQuery;
        return result;
    }
}