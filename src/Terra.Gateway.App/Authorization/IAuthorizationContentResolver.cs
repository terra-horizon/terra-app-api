namespace Terra.Gateway.App.Authorization
{
	public interface IAuthorizationContentResolver
	{
		Boolean HasAuthenticated();

		String CurrentUser();

		Task<Boolean> HasPermission(params String[] permissions);

		ISet<String> PermissionsOfContextRoles(IEnumerable<String> roles);
	}
}
