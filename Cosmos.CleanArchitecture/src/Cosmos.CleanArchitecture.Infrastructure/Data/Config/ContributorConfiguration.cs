using Cosmos.CleanArchitecture.Core.ContributorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cosmos.CleanArchitecture.Infrastructure.Data.Config;

public class ContributorConfiguration : IEntityTypeConfiguration<Contributor>
{
  public void Configure(EntityTypeBuilder<Contributor> builder)
  {
    builder.Property(p => p.Name)
        .HasMaxLength(100)
        .IsRequired();
  }
}
