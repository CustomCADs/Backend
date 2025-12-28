using CustomCADs.Shared.Domain.Bases.Entities;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Files.Domain.Images;

public class Image : BaseAggregateRoot
{
	private Image() { }
	private Image(
		string key,
		string contentType,
		AccountId ownerId
	)
	{
		Key = key;
		ContentType = contentType;
		OwnerId = ownerId;
	}

	public ImageId Id { get; set; }
	public string Key { get; private set; } = string.Empty;
	public string ContentType { get; private set; } = string.Empty;
	public AccountId OwnerId { get; private set; }

	public static Image Create(string key, string contentType, AccountId ownerId)
	=> new Image(key, contentType, ownerId)
		.ValidateKey()
		.ValidateContentType();

	public static Image CreateWithId(ImageId id, string key, string contentType, AccountId ownerId)
		=> new Image(key, contentType, ownerId) { Id = id }
		.ValidateKey()
		.ValidateContentType();

	public Image SetContentType(string contentType)
	{
		ContentType = contentType;
		this.ValidateContentType();

		return this;
	}
}
