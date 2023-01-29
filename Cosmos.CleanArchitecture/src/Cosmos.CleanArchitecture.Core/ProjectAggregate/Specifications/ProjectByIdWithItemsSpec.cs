using Ardalis.Specification;
using Cosmos.CleanArchitecture.Core.ProjectAggregate;

namespace Cosmos.CleanArchitecture.Core.ProjectAggregate.Specifications;

public class ProjectByIdWithItemsSpec : Specification<Project>, ISingleResultSpecification
{
  public ProjectByIdWithItemsSpec(int projectId)
  {
    Query
        .Where(project => project.Id == projectId)
        .Include(project => project.Items);
  }
}
