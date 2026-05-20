using CustomCADs.Modules.Catalog.Domain.Products;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.UnitTests.Catalog.Data.Products;

using static Data.Products.TestData;

public class BaseUnitTests
{
	public static readonly CancellationToken ct = CancellationToken.None;

	protected static Product CreateProduct(
		string? name = null,
		string? description = null,
		decimal? price = null,
		AccountId? creatorId = null,
		CategoryId? categoryId = null,
		ImageId? imageId = null,
		CadId? cadId = null,
		DateTimeOffset? uploadedAt = null,
		ProductId? id = null
	) => Product.CreateWithId(
			name: name ?? MinValidName,
			description: description ?? MinValidDescription,
			price: price ?? MinValidPrice,
			creatorId: creatorId ?? ValidCreatorId,
			categoryId: categoryId ?? ValidCategoryId,
			imageId: imageId ?? ValidImageId,
			cadId: cadId ?? ValidCadId,
			id: id ?? ValidId,
			uploadedAt: uploadedAt
		);
}
