using Microsoft.AspNetCore.Authorization;

namespace Terra.Gateway.Api.Authorization
{
	public class OwnedResourceRequirement : IAuthorizationRequirement
	{
		public OwnedResourceRequirement() { }
	}
}
