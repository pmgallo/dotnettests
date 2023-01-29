using Ardalis.Specification;

namespace Cosmos.CleanArchitecture.SharedKernel.Interfaces;

// from Ardalis.Specification
public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}
