using Cosmos.CleanArchitecture.Core.ContributorAggregate;
using Cosmos.CleanArchitecture.SharedKernel;

namespace Cosmos.CleanArchitecture.Core.ProjectAggregate.Events;

public class ContributorAddedToItemEvent : DomainEventBase
{
  public int ContributorId { get; set; }
  public ToDoItem Item { get; set; }

  public ContributorAddedToItemEvent(ToDoItem item, int contributorId)
  {
    Item = item;
    ContributorId = contributorId;
  }
}
