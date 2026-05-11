using DeviceDetectorNET;
using DeviceDetectorNET.Parser;
using DeviceDetectorNET.Results;
using DeviceDetectorNET.Results.Client;

namespace CustomCADs.Modules.Identity.Infrastructure.Fingerprints;

using Application.Contracts;
using Domain.Users.ValueObjects;

public class DeviceDetectorFingerprintService : IFingerprintService
{
	public DeviceDetectorFingerprintService()
	{
		DeviceDetector.SetVersionTruncation(VersionTruncation.VERSION_TRUNCATION_MINOR);
	}

	public Fingerprint GetFingerprint(Dictionary<string, string?> headers)
		=> new(
			Device: GetDevice(headers),
			Location: GetLocation(headers)
		);

	private static string GetDevice(Dictionary<string, string?> headers)
	{
		DeviceDetector detector = new(
			userAgent: headers["User-Agent"],
			clientHints: ClientHints.Factory(headers)
		);
		detector.Parse();

		if (detector.IsBot())
		{
			IEnumerable<string> botNames = detector.GetBot().Matches.Select(x => x.Name);
			return string.Join("; ", botNames);
		}

		ClientMatchResult clientInfo = detector.GetClient().Match;
		OsMatchResult osInfo = detector.GetOs().Match;

		string os = $"{osInfo.Name} {osInfo.Platform}";
		string client = $"{clientInfo.Name} {clientInfo.Version}";

		return $"{os} running {client}";
	}

	private static string? GetLocation(Dictionary<string, string?> headers, string locationHeaderName = "CF-IPCountry")
		=> headers.GetValueOrDefault(locationHeaderName);
}
