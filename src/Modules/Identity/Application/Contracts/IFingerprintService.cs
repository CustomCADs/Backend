namespace CustomCADs.Modules.Identity.Application.Contracts;

using Domain.Users.ValueObjects;

public interface IFingerprintService
{
	Fingerprint GetFingerprint(Dictionary<string, string?> headers);
}
