using Autofac;
using Cosmos.CleanArchitecture.Core.Interfaces;
using Cosmos.CleanArchitecture.Core.Services;

namespace Cosmos.CleanArchitecture.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<ToDoItemSearchService>()
        .As<IToDoItemSearchService>().InstancePerLifetimeScope();

    builder.RegisterType<DeleteContributorService>()
        .As<IDeleteContributorService>().InstancePerLifetimeScope();
  }
}
