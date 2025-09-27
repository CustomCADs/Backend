using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.Customs.Domain.Customs.States.Implementations;

public class RemovedCustomState : BaseCustomState
{
	public override CustomStatus Status => CustomStatus.Removed;
}
