using Ardalis.Result;
using Cosmos.CleanArchitecture.Core.ProjectAggregate;

namespace Cosmos.CleanArchitecture.Core.Interfaces;

public interface IToDoItemSearchService
{
  Task<Result<ToDoItem>> GetNextIncompleteItemAsync(int projectId);
  Task<Result<List<ToDoItem>>> GetAllIncompleteItemsAsync(int projectId, string searchString);
}
