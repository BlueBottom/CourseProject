using AdvertBoard.Application.AppServices.Contexts.Accounts.Validators.BusinessLogic;
using AdvertBoard.Application.AppServices.Contexts.Users.Models;
using AdvertBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Users.Services;
using AdvertBoard.Application.AppServices.Helpers;
using AdvertBoard.Contracts.Contexts.Accounts.Requests;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using AutoFixture;
using Bogus;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace AdvertBoard.Tests.Application.AppServicesTests.Contexts.Authentication.Validators.BusinessLogic;

/// <summary>
/// Тесты для валидатора бизнес логики при регистрации пользователя.
/// </summary>
public class RegisterUserValidatorTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly RegisterUserValidator _validator;
    private readonly CancellationToken _token;
    private readonly Fixture _fixture;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="RegisterUserValidatorTests"/>.
    /// </summary>
    public RegisterUserValidatorTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _validator = new RegisterUserValidator(_userRepositoryMock.Object);
        _token = new CancellationTokenSource().Token;       
        _fixture = new Fixture();
    }   

    /// <summary>
    /// Выдает ошибку, если пользователь с таким email уже существует.
    /// </summary>
    [Fact]
    public async Task Should_Have_Error_When_Email_Already_Exists()
    {
        // Arrange
        var email = _fixture.Create<string>();
        
        var request = _fixture
            .Build<RegisterUserRequest>()
            .With(x => x.Email, email)
            .Create();
        
        _userRepositoryMock
            .Setup(x => x.FindByEmail(email, _token))
            .ReturnsAsync(new UserWithPasswordModel());

        // Act
        var result = await _validator.TestValidateAsync(request, cancellationToken: _token);

        // Assert
        result
                .ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("Пользователь с таким email уже существует.");
    }    
    
    /// <summary>
    /// Не выдает ошибок, если email еще не был добавлен.
    /// </summary>
    [Fact]
    public async Task Should_Not_Have_Error_When_Email_Does_Not_Exist()
    {
        // Arrange
        var email = _fixture.Create<string>();

        UserWithPasswordModel? model = null;
        
        var request = _fixture
            .Build<RegisterUserRequest>()
            .With(x => x.Email, email)
            .Create();
        
        _userRepositoryMock
            .Setup(x => x.FindByEmail(email, _token))
            .ReturnsAsync(model);
        
        // Act
        var result = await _validator.TestValidateAsync(request, cancellationToken: _token);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
    }    
    
    /// <summary>
    /// Выдает ошибку, если пользователь с таким номером телефона уже существует.
    /// </summary>
    [Fact]
    public async Task Should_Have_Error_When_Phone_Already_Exists()
    {
        // Arrange
        var phone = new Faker().Phone.PhoneNumber("8(###)###-##-##");
        var normalizedPhone = PhoneHelper.NormalizePhoneNumber(phone);
        
        var request = _fixture
            .Build<RegisterUserRequest>()
            .With(x => x.Phone, phone)
            .Create();
        
        _userRepositoryMock
            .Setup(x => x.IsExistByPhone(normalizedPhone, _token))
            .ReturnsAsync(true);

        // Act
        var result = await _validator.TestValidateAsync(request, cancellationToken: _token);

        // Assert
        result
                .ShouldHaveValidationErrorFor(x => x.Phone)
                .WithErrorMessage("Пользователь с таким номером телефона уже существует.");
    }    
    
    /// <summary>
    /// Не выжает ошибок, если номер телефона еще не был добавлен.
    /// </summary>
    [Fact]
    public async Task Should_Not_Have_Error_When_Phone_Does_Not_Exist()
    {
        // Arrange
        var phone = _fixture.Create<string>();
        
        var request = _fixture
            .Build<RegisterUserRequest>()
            .With(x => x.Phone, phone)
            .Create();
        
        _userRepositoryMock
            .Setup(x => x.IsExistByPhone(phone, _token))
            .ReturnsAsync(false);
        
        // Act
        var result = await _validator.TestValidateAsync(request, cancellationToken: _token);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Phone);
    }
}