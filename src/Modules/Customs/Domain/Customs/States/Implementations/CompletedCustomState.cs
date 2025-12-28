using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.Modules.Customs.Domain.Customs.States.Implementations;

public class CompletedCustomState : BaseCustomState
{
	public override CustomStatus Status => CustomStatus.Completed;
}
