using Cite.Tools.Common.Extensions;
using Cite.Tools.Data.Query;
using Cite.Tools.FieldSet;
using Cite.Tools.Logging;
using Cite.Tools.Logging.Extensions;
using Cite.WebTools.CurrentPrincipal;
using Terra.Gateway.Api.Model;
using Terra.Gateway.Api.OpenApi;
using Terra.Gateway.Api.Validation;
using Terra.Gateway.App.Accounting;
using Terra.Gateway.App.Authorization;
using Terra.Gateway.App.ErrorCode;
using Terra.Gateway.App.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace Terra.Gateway.Api.Controllers
{
	[Route("api/principal")]
	public class PrincipalController : ControllerBase
	{
		private readonly ICurrentPrincipalResolverService _currentPrincipalResolverService;
		private readonly ILogger<PrincipalController> _logger;
		private readonly AccountBuilder _accountBuilder;
		private readonly IAuthorizationContentResolver _authorizationContentResolver;
		private readonly App.Authorization.IAuthorizationService _authorizationService;
		private readonly ErrorThesaurus _errors;
		private readonly IAccountingService _accountingService;
		private readonly IStringLocalizer<Terra.Gateway.Resources.MySharedResources> _localizer;
		private readonly EventBroker _eventBroker;
		private readonly QueryFactory _queryFactory;

		public PrincipalController(
			ILogger<PrincipalController> logger,
			ICurrentPrincipalResolverService currentPrincipalResolverService,
			IAuthorizationContentResolver authorizationContentResolver,
			App.Authorization.IAuthorizationService authorizationService,
			IStringLocalizer<Terra.Gateway.Resources.MySharedResources> localizer,
			IAccountingService accountingService,
			QueryFactory queryFactory,
			ErrorThesaurus errors,
			EventBroker eventBroker,
			AccountBuilder accountBuilder)
		{
			this._logger = logger;
			this._currentPrincipalResolverService = currentPrincipalResolverService;
			this._authorizationContentResolver = authorizationContentResolver;
			this._authorizationService = authorizationService;
			this._accountingService = accountingService;
			this._accountBuilder = accountBuilder;
			this._queryFactory = queryFactory;
			this._errors = errors;
			this._localizer = localizer;
			this._eventBroker = eventBroker;
		}

		[HttpGet("me")]
		[Authorize]
		[ModelStateValidationFilter]
		[SwaggerOperation(Summary = "Retrieve information for the logged in user")]
		[SwaggerResponse(statusCode: 200, description: "The information available for the logged in user", type: typeof(Account))]
		[SwaggerResponse(statusCode: 401, description: "The request is not authenticated")]
		[SwaggerResponse(statusCode: 500, description: "Internal error")]
		[SwaggerResponse(statusCode: 503, description: "An underpinning service indicated failure")]
		[Produces(System.Net.Mime.MediaTypeNames.Application.Json)]
		public async Task<Account> Me(
			[ModelBinder(Name = "f")]
			[SwaggerParameter(description: "The fields to include in the response model", Required = false)]
			[LookupFieldSetQueryStringOpenApi]
			IFieldSet fieldSet)
		{
			this._logger.Debug(new MapLogEntry("me").And("fields", fieldSet));
			if (fieldSet == null || fieldSet.IsEmpty()) fieldSet = new FieldSet(
				nameof(Account.UserId),
				nameof(Account.IsAuthenticated),
				nameof(Account.Roles),
				nameof(Account.Permissions),
				nameof(Account.DeferredPermissions),
				nameof(Account.More),
				new String[] { nameof(Account.Principal), nameof(Account.PrincipalInfo.Subject) }.AsIndexer(),
				new String[] { nameof(Account.Principal), nameof(Account.PrincipalInfo.Name) }.AsIndexer(),
				new String[] { nameof(Account.Principal), nameof(Account.PrincipalInfo.Username) }.AsIndexer(),
				new String[] { nameof(Account.Principal), nameof(Account.PrincipalInfo.GivenName) }.AsIndexer(),
				new String[] { nameof(Account.Principal), nameof(Account.PrincipalInfo.FamilyName) }.AsIndexer(),
				new String[] { nameof(Account.Principal), nameof(Account.PrincipalInfo.Email) }.AsIndexer(),
				new String[] { nameof(Account.Token), nameof(Account.TokenInfo.Client) }.AsIndexer(),
				new String[] { nameof(Account.Token), nameof(Account.TokenInfo.Issuer) }.AsIndexer(),
				new String[] { nameof(Account.Token), nameof(Account.TokenInfo.TokenType) }.AsIndexer(),
				new String[] { nameof(Account.Token), nameof(Account.TokenInfo.AuthorizedParty) }.AsIndexer(),
				new String[] { nameof(Account.Token), nameof(Account.TokenInfo.Audience) }.AsIndexer(),
				new String[] { nameof(Account.Token), nameof(Account.TokenInfo.ExpiresAt) }.AsIndexer(),
				new String[] { nameof(Account.Token), nameof(Account.TokenInfo.IssuedAt) }.AsIndexer(),
				new String[] { nameof(Account.Token), nameof(Account.TokenInfo.Scope) }.AsIndexer());

			ClaimsPrincipal principal = this._currentPrincipalResolverService.CurrentPrincipal();

			Account me = await this._accountBuilder.Build(fieldSet, principal);

			return me;
		}
	}
}
