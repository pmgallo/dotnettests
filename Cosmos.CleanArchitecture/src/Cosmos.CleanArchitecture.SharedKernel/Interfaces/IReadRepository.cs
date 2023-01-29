using Ardalis.Specification;

namespace Cosmos.CleanArchitecture.SharedKernel.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
