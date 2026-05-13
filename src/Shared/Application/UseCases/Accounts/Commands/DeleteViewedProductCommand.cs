namespace CustomCADs.Shared.Application.UseCases.Accounts.Commands;

public record DeleteViewedProductCommand(
	ProductId ProductId,
	AccountId CallerId
) : ICommand;
