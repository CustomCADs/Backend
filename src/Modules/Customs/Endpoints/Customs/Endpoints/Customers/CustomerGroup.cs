namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers;

using static DomainConstants.Roles;
using static EndpointsConstants;

public class CustomerGroup : Group
{
	public CustomerGroup()
	{
		Configure(Paths.CustomsCustomer, x =>
		{
			x.Roles(Customer);
			x.Description(x => x.WithTags(Tags[Paths.CustomsCustomer]));
		});
	}
}
