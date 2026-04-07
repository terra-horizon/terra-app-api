namespace Terra.Gateway.App.Authorization
{
	public interface IAuthorizationContentResolver
	{
		Boolean HasAuthenticated();

		String CurrentUser();

		Task<Boolean> HasPermission(params String[] permissions);

		ISet<String> PermissionsOfContextRoles(IEnumerable<String> roles);


		Task<HashSet<String>> ContextRolesForCollectionOfUser(String subjectId, Guid collectionId);


		Task<Dictionary<Guid, HashSet<String>>> ContextRolesForCollectionOfUser(String subjectId, IEnumerable<Guid> collectionIds);

		Task<HashSet<String>> ContextRolesForCollectionOfUserGroup(String groupId, Guid collectionId);

		Task<Dictionary<Guid, HashSet<String>>> ContextRolesForCollectionOfUserGroup(String groupId, IEnumerable<Guid> collectionIds);

		Task<Dictionary<Guid, HashSet<String>>> EffectiveContextRolesForDatasetOfUser(String subjectId, IEnumerable<Guid> datasetIds);

		Task<Dictionary<Guid, HashSet<String>>> EffectiveContextRolesForDatasetOfUserGroup(String groupId, IEnumerable<Guid> datasetIds);
	}
}
