using CustomCADs.Modules.Accounts.Application.Accounts.Commands.Shared;
using CustomCADs.Modules.Accounts.Domain.Repositories;
using CustomCADs.Modules.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.UseCases.Accounts.Commands;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Shared.DeleteViewedProduct;

using static Data.Accounts.TestData;

public class Tests : Data.Accounts.BaseUnitTests
{
	private readonly DeleteViewedProductHandler handler;
	private readonly DeleteViewedProductCommand request = new(ValidProductId, ValidId);

	private readonly Mock<IAccountWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	public Tests()
	{
		handler = new(writes.Object, uow.Object);
	}

	[Test]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		writes.Verify(
			x => x.UnviewProductAsync(ValidId, ValidProductId, ct),
			Times.Once()
		);
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}
}
