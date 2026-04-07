using Cite.Tools.Auth.Claims;
using Cite.Tools.Common.Extensions;
using Cite.Tools.Data.Query;
using Cite.WebTools.CurrentPrincipal;
using Terra.Gateway.App.Common.Auth;
using Terra.Gateway.App.Service.AAI;

namespace Terra.Gateway.App.Authorization
{
    public class AuthorizationContentResolver : IAuthorizationContentResolver
	{
		private readonly ICurrentPrincipalResolverService _currentPrincipalResolverService;
		private readonly IAuthorizationService _authorizationService;
		private readonly ClaimExtractor _extractor;
		private readonly IPermissionPolicyService _permissionPolicyService;
		private readonly QueryFactory _queryFactory;
		private readonly IAAIService _aaiService;

		public AuthorizationContentResolver(
			ICurrentPrincipalResolverService currentPrincipalResolverService,
			IAuthorizationService authorizationService,
			IPermissionPolicyService permissionPolicyService,
			IAAIService aaiService,
			QueryFactory queryFactory,
			ClaimExtractor extractor)
		{
			this._currentPrincipalResolverService = currentPrincipalResolverService;
			this._authorizationService = authorizationService;
			this._permissionPolicyService = permissionPolicyService;
			this._queryFactory = queryFactory;
			this._aaiService = aaiService;
			this._extractor = extractor;
		}

		public Boolean HasAuthenticated()
		{
			return this._currentPrincipalResolverService.CurrentPrincipal() != null;
		}

		public String CurrentUser()
		{
			String currentUser = this._extractor.SubjectString(this._currentPrincipalResolverService.CurrentPrincipal());
			return currentUser;
		}

		public async Task<Boolean> HasPermission(params String[] permissions)
		{
			return await this._authorizationService.Authorize(permissions);
		}

		public ISet<String> PermissionsOfContextRoles(IEnumerable<String> roles)
		{
			return this._permissionPolicyService.PermissionsOfContext(roles);
		}


		public async Task<HashSet<String>> ContextRolesForCollectionOfUser(String subjectId, Guid collectionId)
		{
			Dictionary<Guid, HashSet<String>> rolesByCollection= await this.ContextRolesForCollectionOfUser(subjectId, [collectionId]);
			if(rolesByCollection == null || !rolesByCollection.ContainsKey(collectionId)) return Enumerable.Empty<String>().ToHashSet();
			return rolesByCollection[collectionId];
		}


		public async Task<Dictionary<Guid, HashSet<String>>> ContextRolesForCollectionOfUser(String subjectId, IEnumerable<Guid> collectionIds)
		{
			if (collectionIds == null || !collectionIds.Any()) return new Dictionary<Guid, HashSet<string>>();

			HashSet<Guid> collectionIdsMap = collectionIds.ToHashSet();

			List<ContextGrant> grants = await this._aaiService.LookupUserContextGrants(subjectId);
			Dictionary<Guid, HashSet<String>> rolesByCollection = grants
				.Where(x => x.TargetType == ContextGrant.TargetKind.Collection && collectionIdsMap.Contains(x.TargetId))
				.ToDictionaryOfList(x => x.TargetId)
				.ToDictionary(x => x.Key, x => x.Value.Select(y => y.Role).ToHashSet());
			return rolesByCollection;
		}

		public async Task<HashSet<String>> ContextRolesForCollectionOfUserGroup(String groupId, Guid collectionId)
		{
			Dictionary<Guid, HashSet<String>> rolesByCollection = await this.ContextRolesForCollectionOfUserGroup(groupId, [collectionId]);
			if (rolesByCollection == null || !rolesByCollection.ContainsKey(collectionId)) return Enumerable.Empty<String>().ToHashSet();
			return rolesByCollection[collectionId];
		}

		public async Task<Dictionary<Guid, HashSet<String>>> ContextRolesForCollectionOfUserGroup(String groupId, IEnumerable<Guid> collectionIds)
		{
			if (collectionIds == null || !collectionIds.Any()) return new Dictionary<Guid, HashSet<string>>();

			HashSet<Guid> collectionIdsMap = collectionIds.ToHashSet();

			List<ContextGrant> grants = await this._aaiService.LookupUserGroupContextGrants(groupId);
			Dictionary<Guid, HashSet<String>> rolesByCollection = grants
				.Where(x => x.TargetType == ContextGrant.TargetKind.Collection && collectionIdsMap.Contains(x.TargetId))
				.ToDictionaryOfList(x => x.TargetId)
				.ToDictionary(x => x.Key, x => x.Value.Select(y => y.Role).ToHashSet());
			return rolesByCollection;
		}


		public async Task<Dictionary<Guid, HashSet<String>>> EffectiveContextRolesForDatasetOfUser(String subjectId, IEnumerable<Guid> datasetIds)
		{
			if (datasetIds == null || !datasetIds.Any()) return new Dictionary<Guid, HashSet<string>>();

			HashSet<Guid> datasetIdsMap = datasetIds.ToHashSet();
			List<ContextGrant> grants = await this._aaiService.LookupUserContextGrants(subjectId);
			Dictionary<Guid, HashSet<String>> rolesByDataset = grants
				.Where(x => x.TargetType == ContextGrant.TargetKind.Dataset && datasetIdsMap.Contains(x.TargetId))
				.ToDictionaryOfList(x => x.TargetId)
				.ToDictionary(x => x.Key, x => x.Value.Select(y => y.Role).ToHashSet());

			return rolesByDataset;
		}

		public async Task<Dictionary<Guid, HashSet<String>>> EffectiveContextRolesForDatasetOfUserGroup(String groupId, IEnumerable<Guid> datasetIds)
		{
			if (datasetIds == null || !datasetIds.Any()) return new Dictionary<Guid, HashSet<string>>();

			HashSet<Guid> datasetIdsMap = datasetIds.ToHashSet();
			List<ContextGrant> grants = await this._aaiService.LookupUserGroupContextGrants(groupId);
			Dictionary<Guid, HashSet<String>> rolesByDataset = grants
				.Where(x => x.TargetType == ContextGrant.TargetKind.Dataset && datasetIdsMap.Contains(x.TargetId))
				.ToDictionaryOfList(x => x.TargetId)
				.ToDictionary(x => x.Key, x => x.Value.Select(y => y.Role).ToHashSet());

			return rolesByDataset;
		}
	}
}
