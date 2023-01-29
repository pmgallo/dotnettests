using Ardalis.Specification;

namespace Cosmos.CleanArchitecture.Core.ProjectAggregate.Specifications;

public class ProjectsWithItemsByContributorIdSpec : Specification<Project>, ISingleResultSpecification
{
  public ProjectsWithItemsByContributorIdSpec(int contributorId)
  {
    Query
        .Where(project => project.Items.Where(item => item.ContributorId == contributorId).Any())
        .Include(project => project.Items);
  }
}
