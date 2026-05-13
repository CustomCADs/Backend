namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Customers;

using static APIConstants;
using static DomainConstants.Users;

public class CustomerGroup : SubGroup<CustomsGroup>
{
	public CustomerGroup()
	{
		Configure(Paths.Customer, x =>
		{
			x.Roles(CustomerRole);
			x.Description(x => x.WithTags(Tags[$"{Paths.Customs}/{Paths.Customer}"]));
		});
	}
}
