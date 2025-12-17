using CustomCADs.Modules.Customs.Domain.Customs.States.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Customs.Persistence.Configurations.Customs.States.Accepted;

public class Configurations : IEntityTypeConfiguration<AcceptedCustom>
{
	public void Configure(EntityTypeBuilder<AcceptedCustom> builder)
		=> builder
			.SetPrimaryKey()
			.SetStronglyTypedIds()
			.SetNavigations()
			.SetValidations();
}
