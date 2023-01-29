using Ardalis.Result;

namespace Cosmos.CleanArchitecture.Core.Interfaces;

public interface IDeleteContributorService
{
    public Task<Result> DeleteContributor(int contributorId);
}
