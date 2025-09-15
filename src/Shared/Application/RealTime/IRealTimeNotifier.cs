namespace CustomCADs.Shared.Application.RealTime;

public interface IRealTimeNotifier
{
	Task NotifyAllAsync<TPayload>(string message, TPayload payload, CancellationToken ct = default);
	Task NotifyGroupAsync<TPayload>(string group, string message, TPayload payload, CancellationToken ct = default);
	Task NotifyGroupsAsync<TPayload>(IReadOnlyList<string> groups, string message, TPayload payload, CancellationToken ct = default);
	Task NotifyUserAsync<TPayload>(AccountId id, string message, TPayload payload, CancellationToken ct = default);
	Task NotifyUsersAsync<TPayload>(IReadOnlyList<AccountId> ids, string message, TPayload payload, CancellationToken ct = default);
}
