using CustomCADs.Modules.Delivery.Domain.Shipments;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Delivery;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Delivery.Persistence.Configurations.Shipments;

internal static class Utilities
{
	extension(EntityTypeBuilder<Shipment> builder)
	{
		internal EntityTypeBuilder<Shipment> SetPrimaryKey()
		{
			builder.HasKey(x => x.Id);

			return builder;
		}

		internal EntityTypeBuilder<Shipment> SetStronglyTypedIds()
		{
			builder.Property(x => x.Id)
				.ValueGeneratedOnAdd()
				.HasConversion(
					x => x.Value,
					x => ShipmentId.New(x)
				);

			builder.Property(x => x.BuyerId)
				.ValueGeneratedOnAdd()
				.HasConversion(
					x => x.Value,
					x => AccountId.New(x)
				);

			return builder;
		}

		internal EntityTypeBuilder<Shipment> SetValueObjects()
		{
			builder.ComplexProperty(x => x.Address, a =>
			{
				a.Property(x => x.Country)
					.IsRequired()
					.HasColumnName(nameof(Shipment.Address.Country));

				a.Property(x => x.City)
					.IsRequired()
					.HasColumnName(nameof(Shipment.Address.City));

				a.Property(x => x.Street)
					.IsRequired()
					.HasColumnName(nameof(Shipment.Address.Street));
			});

			builder.ComplexProperty(x => x.Info, a =>
			{
				a.Property(x => x.Recipient)
					.IsRequired()
					.HasColumnName(nameof(Shipment.Info.Recipient));

				a.Property(x => x.Count)
					.IsRequired()
					.HasColumnName(nameof(Shipment.Info.Count));

				a.Property(x => x.Weight)
					.IsRequired()
					.HasColumnName(nameof(Shipment.Info.Weight));
			});

			builder.ComplexProperty(x => x.Reference, a =>
			{
				a.Property(x => x.Id)
					.HasColumnName("ReferenceId");

				a.Property(x => x.Service)
					.IsRequired()
					.HasColumnName(nameof(Shipment.Reference.Service));
			});

			builder.ComplexProperty(x => x.Contact, a =>
			{
				a.Property(x => x.Email)
					.HasColumnName(nameof(Shipment.Contact.Email));

				a.Property(x => x.Phone)
					.HasColumnName(nameof(Shipment.Contact.Phone));
			});

			return builder;
		}

		internal EntityTypeBuilder<Shipment> SetValidations()
		{
			builder.Property(x => x.RequestedAt)
				.IsRequired()
				.HasColumnName(nameof(Shipment.RequestedAt));

			builder.Property(x => x.BuyerId)
				.IsRequired()
				.HasColumnName(nameof(Shipment.BuyerId));

			return builder;
		}
	}

}
