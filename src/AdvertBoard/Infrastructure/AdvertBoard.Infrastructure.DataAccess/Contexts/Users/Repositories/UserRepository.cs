using System.Linq.Expressions;
using AdvertBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertBoard.Contracts.Contexts.Users;
using AdvertBoard.Domain.Contexts.Users;
using AdvertBoard.Infrastructure.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdvertBoard.Infrastructure.DataAccess.Contexts.Users.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IRepository<User> _repository;
    private readonly IMapper _mapper;

    public UserRepository(IRepository<User> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<User?> FindUser(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _repository.GetAllFiltered(predicate).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Guid> AddUser(RegisterUserDto registerUserDto, string password, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<RegisterUserDto, User>(registerUserDto);
        user.Password = password;
        await _repository.AddAsync(user, cancellationToken);
        return user.Id;
    }
}