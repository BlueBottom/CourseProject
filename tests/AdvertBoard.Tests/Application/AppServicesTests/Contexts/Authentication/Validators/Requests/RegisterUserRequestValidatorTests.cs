using AdvertBoard.Application.AppServices.Contexts.Authentication.Validators.Requests;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using AdvertBoard.Tests.Application.AppServicesTests.Data;
using FluentValidation.TestHelper;
using Xunit;

namespace AdvertBoard.Tests.Application.AppServicesTests.Contexts.Authentication.Validators.Requests;

/// <summary>
/// Тесты для валидатора запроса на регистрацию пользователя.
/// </summary>
public class RegisterUserRequestValidatorTests
{
    private readonly RegisterUserRequestValidator _validator;
    
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="RegisterUserRequestValidatorTests"/>.
    /// </summary>
    public RegisterUserRequestValidatorTests()
    {
        _validator = new RegisterUserRequestValidator();
    }
    
    /// <summary>
    /// Тест для проверки невалидных имен.
    /// </summary>
    /// <param name="name">Имя пользователя.</param>
    [Theory]
    [MemberData(nameof(DataHelper.GetInvalidNames), MemberType = typeof(DataHelper))]
    public void Should_Have_Validation_Error_In_Name(string name)
    {
        //Arrange
        var request = new RegisterUserRequest
        {
            Name = name
        };
        
        //Act
        var result = _validator.TestValidate(request);
        
        //Result
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    /// <summary>
    /// Тест для проверки валидных имен.
    /// </summary>
    /// <param name="name">Имя пользователя.</param>
    [Theory]
    [MemberData(nameof(DataHelper.GetValidNames), MemberType = typeof(DataHelper))]
    public void Should_Not_Have_Validation_Errors_In_Name(string name)
    {
        //Arrange
        var request = new RegisterUserRequest
        {
            Name = name
        };
        
        //Act 
        var result = _validator.TestValidate(request);
        
        //Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }
    
    /// <summary>
    /// Тест для проверки невалидных фамилий.
    /// </summary>
    /// <param name="lastname">Фамилия.</param>
    [Theory]
    [MemberData(nameof(DataHelper.GetInvalidLastnames), MemberType = typeof(DataHelper))]   
    public void Should_Have_Validation_Error_In_Lastname(string lastname)
    {
        //Arrange
        var request = new RegisterUserRequest
        {
            Lastname = lastname
        };
        
        //Act 
        var result = _validator.TestValidate(request);
        
        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Lastname);
    } 
    
    /// <summary>
    /// Тест для проверки валидных фамилий.
    /// </summary>
    /// <param name="lastname">Фамилия.</param>
    [Theory]
    [MemberData(nameof(DataHelper.GetValidLastnames), MemberType = typeof(DataHelper))]
    public void Should_Not_Have_Validation_Error_In_Lastname(string lastname)
    {
        //Arrange
        var request = new RegisterUserRequest
        {
            Lastname = lastname
        };
        
        //Act 
        var result = _validator.TestValidate(request);
        
        //Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Lastname);
    } 
    
    /// <summary>
    /// Тест для проверки невалидных адресов электронной почты.
    /// </summary>
    /// <param name="email">Адрес электронной почты.</param>
    [Theory]
    [MemberData(nameof(DataHelper.GetInvalidEmails), MemberType = typeof(DataHelper))]
    public void Should_Have_Validation_Error_In_Email(string email)
    {
        //Arrange
        var request = new RegisterUserRequest
        {
            Email = email
        };
        
        //Act 
        var result = _validator.TestValidate(request);
        
        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    } 
    
    /// <summary>
    /// Тест для проверки валидных адресов электронной почты.
    /// </summary>
    /// <param name="email">Адрес электронной почты.</param>
    [Theory]
    [MemberData(nameof(DataHelper.GetValidEmails), MemberType = typeof(DataHelper))]
    public void Should_Not_Have_Validation_Error_In_Email(string email)
    {
        //Arrange
        var request = new RegisterUserRequest
        {
            Email = email
        };
        
        //Act 
        var result = _validator.TestValidate(request);
        
        //Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
    }   
    
    /// <summary>
    /// Тест для проверки невалидных номеров телефона.
    /// </summary>
    /// <param name="phone">Номер телефона.</param>
    [Theory]
    [MemberData(nameof(DataHelper.GetInvalidPhones), MemberType = typeof(DataHelper))]
    public void Should_Have_Validation_Error_In_Phone(string phone)
    {
        //Arrange
        var request = new RegisterUserRequest
        {
            Phone = phone
        };
        
        //Act 
        var result = _validator.TestValidate(request);
        
        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Phone);
    } 
    
    /// <summary>
    /// Тест для проверки валидных номеров телефона.
    /// </summary>
    /// <param name="phone">Номер телефона.</param>
    [Theory]
    [MemberData(nameof(DataHelper.GetValidPhones), MemberType = typeof(DataHelper))]
    public void Should_Not_Have_Validation_Error_In_Phone(string phone)
    {
        //Arrange
        var request = new RegisterUserRequest
        {
            Phone = phone
        };
        
        //Act 
        var result = _validator.TestValidate(request);
        
        //Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Phone);
    }
    
    /// <summary>
    /// Тест для проверки невалидных паролей.
    /// </summary>
    /// <param name="password">Пароль.</param>
    [Theory]
    [MemberData(nameof(DataHelper.GetInvalidPasswords), MemberType = typeof(DataHelper))]
    public void Should_Have_Validation_Error_In_Password(string password)
    {
        //Arrange
        var request = new RegisterUserRequest
        {
            Password = password
        };
        
        //Act 
        var result = _validator.TestValidate(request);
        
        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    } 
    
    /// <summary>
    /// Тест для проверки валидных паролей.
    /// </summary>
    /// <param name="password">Пароль.</param>
    [Theory]
    [MemberData(nameof(DataHelper.GetValidPasswords), MemberType = typeof(DataHelper))]
    public void Should_Not_Have_Validation_Error_In_Password(string password)
    {
        //Arrange
        var request = new RegisterUserRequest
        {
            Password = password
        };
        
        //Act 
        var result = _validator.TestValidate(request);
        
        //Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Password);
    }
}