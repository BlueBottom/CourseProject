using System.Linq.Expressions;
using AdvertBoard.Application.AppServices.Specifications.Extensions;

namespace AdvertBoard.Application.AppServices.Specifications.Internal
{
    /// <summary>
    /// Спецификация логического отрицания.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    internal class NotSpecification<TEntity> : Specification<TEntity>
    {
        /// <inheritdoc />
        public override Expression<Func<TEntity, bool>> PredicateExpression { get; }

        /// <summary>
        /// Инициализирует экземпляр <see cref="NotSpecification{TEntity}"/>.
        /// </summary>
        public NotSpecification(ISpecification<TEntity> specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            PredicateExpression = specification.PredicateExpression.Not();
        }
    }
}