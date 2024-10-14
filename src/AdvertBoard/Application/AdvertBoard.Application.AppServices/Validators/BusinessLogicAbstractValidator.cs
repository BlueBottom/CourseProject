using FluentValidation;

namespace AdvertBoard.Application.AppServices.Validators;

/// <summary>
/// Абстрактный класс, от которого наследуются валидаторы бизнес логики.
/// </summary>
/// <typeparam name="T">Тип валидируемого объекта.</typeparam>
public abstract class BusinessLogicAbstractValidator<T> : AbstractValidator<T>
{
    
}