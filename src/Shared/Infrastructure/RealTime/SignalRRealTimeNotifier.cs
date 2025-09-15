using CustomCADs.Shared.Application.RealTime;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using Microsoft.AspNetCore.SignalR;

namespace CustomCADs.Shared.Infrastructure.RealTime;

public class SignalRRealTimeNotifier<THub>(IHubContext<THub> hub) : IRealTimeNotifier where THub : Hub
{
	public Task NotifyAllAsync<TPayload>(string method, TPayload payload, CancellationToken ct = default)
		=> hub.Clients
			.All
			.SendAsync(
				method: method,
				arg1: payload,
				cancellationToken: ct
			);

	public Task NotifyGroupAsync<TPayload>(string group, string method, TPayload payload, CancellationToken ct = default)
		=> hub.Clients
			.Group(groupName: group)
			.SendAsync(
				method: method,
				arg1: payload,
				cancellationToken: ct
			);

	public Task NotifyGroupsAsync<TPayload>(IReadOnlyList<string> groups, string method, TPayload payload, CancellationToken ct = default)
		=> hub.Clients
			.Groups(groupNames: groups)
			.SendAsync(
				method: method,
				arg1: payload,
				cancellationToken: ct
			);

	public Task NotifyUserAsync<TPayload>(AccountId id, string method, TPayload payload, CancellationToken ct = default)
		=> hub.Clients
			.User(userId: id.Value.ToString())
			.SendAsync(
				method: method,
				arg1: payload,
				cancellationToken: ct
			);

	public Task NotifyUsersAsync<TPayload>(IReadOnlyList<AccountId> ids, string method, TPayload payload, CancellationToken ct = default)
		=> hub.Clients
			.Users(userIds: [.. ids.Select(x => x.Value.ToString())])
			.SendAsync(
				method: method,
				arg1: payload,
				cancellationToken: ct
			);
}
