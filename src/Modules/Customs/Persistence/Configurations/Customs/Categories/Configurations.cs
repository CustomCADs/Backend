using CustomCADs.Customs.Domain.Customs.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Customs.Persistence.Configurations.Customs.Categories;

public class Configurations : IEntityTypeConfiguration<CustomCategory>
{
	public void Configure(EntityTypeBuilder<CustomCategory> builder)
		=> builder
			.SetPrimaryKey()
			.SetStronglyTypedIds()
			.SetValidations();
}
