using CustomCADs.Shared.Application;
using CustomCADs.Shared.Application.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Printing.Application.Materials.Queries.Internal.GetTextureUrl.Put;

using static ApplicationConstants.FluentMessages;

public class GetMaterialTexturePresignedUrlPutValidator : QueryValidator<GetMaterialTexturePresignedUrlPutQuery, string>
{
	public GetMaterialTexturePresignedUrlPutValidator()
	{
		RuleFor(x => x.NewImage.ContentType)
			.NotEmpty().WithMessage(RequiredError);

		RuleFor(x => x.NewImage.FileName)
			.NotEmpty().WithMessage(RequiredError);
	}
}
