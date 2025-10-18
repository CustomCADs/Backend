namespace CustomCADs.Customs.API.Customs.Endpoints.Customers;

using static APIConstants;
using static DomainConstants.Roles;

public class CustomerGroup : SubGroup<CustomsGroup>
{
	public CustomerGroup()
	{
		Configure(Paths.Customer, x =>
		{
			x.Roles(Customer);
			x.Description(x => x.WithTags(Tags[$"{Paths.Customs}/{Paths.Customer}"]));
		});
	}
}
