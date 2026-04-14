using Cite.Tools.Data.Builder;
using Cite.Tools.Data.Deleter;
using Cite.Tools.Data.Query;
using Cite.Tools.FieldSet;
using Cite.Tools.Json;
using Cite.Tools.Logging;
using Cite.Tools.Logging.Extensions;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Terra.Gateway.App.Authorization;
using Terra.Gateway.App.ErrorCode;
using Terra.Gateway.App.Event;
using Terra.Gateway.App.Exception;
using Terra.Gateway.App.Model;
using Terra.Gateway.App.Query;
using Terra.Gateway.App.Service.Airflow;
using Terra.Gateway.App.Service.Airflow.Model;

namespace Terra.Gateway.App.Service.AiModelRegistry
{
	public class AiModelRegistryService : IAiModelRegistryService
	{
		private readonly BuilderFactory _builderFactory;
		private readonly DeleterFactory _deleterFactory;
		private readonly QueryFactory _queryFactory;
		private readonly IStringLocalizer<Resources.MySharedResources> _localizer;
		private readonly IAuthorizationService _authorizationService;
		private readonly IAuthorizationContentResolver _authorizationContentResolver;
		private readonly ILogger<AiModelRegistryService> _logger;
		private readonly ErrorThesaurus _errors;
		private readonly EventBroker _eventBroker;
		private readonly IAirflowService _airflowService;
		private readonly JsonHandlingService _jsonHandlingService;

		public AiModelRegistryService(
			ILogger<AiModelRegistryService> logger,
			BuilderFactory builderFactory,
			DeleterFactory deleterFactory,
			QueryFactory queryFactory,
			IAuthorizationService authorizationService,
			IAuthorizationContentResolver authorizationContentResolver,
			IStringLocalizer<Resources.MySharedResources> localizer,
			ErrorThesaurus errors,
			EventBroker eventBroker,
			IAirflowService airflowService,
			JsonHandlingService jsonHandlingService)
		{
			this._logger = logger;
			this._builderFactory = builderFactory;
			this._deleterFactory = deleterFactory;
			this._queryFactory = queryFactory;
			this._authorizationService = authorizationService;
			this._authorizationContentResolver = authorizationContentResolver;
			this._localizer = localizer;
			this._errors = errors;
			this._eventBroker = eventBroker;
			this._airflowService = airflowService;
			this._jsonHandlingService = jsonHandlingService;
		}

		public async Task<string> InferAsync(string file, IFieldSet fields = null)
		{
			this._logger.Debug(new MapLogEntry("infering").And("file", file));
			await this._authorizationService.AuthorizeForce(Permission.CanExecuteImageInference);
			List<Airflow.Model.AirflowDag> definitions = await this._queryFactory.Query<WorkflowDefinitionHttpQuery>()
				.Kinds(Common.WorkflowDefinitionKind.AiModelRegistryInference)
				.ExcludeStaled(true)
				.CollectAsync();

			if (definitions == null || definitions.Count == 0) throw new TerraNotFoundException(this._localizer["general_notFound", Common.WorkflowDefinitionKind.AiModelRegistryInference.ToString(), nameof(Model.WorkflowDefinition)]);
			if (definitions.Count > 1) throw new TerraFoundManyException(this._localizer["general_nonUnique", Common.WorkflowDefinitionKind.AiModelRegistryInference.ToString(), nameof(Model.WorkflowDefinition)]);
			Airflow.Model.AirflowDag selectedDefinition = definitions.FirstOrDefault();
			WorkflowExecution workflowExecution = await this._airflowService.ExecuteWorkflowAsync(new Model.WorkflowExecutionArgs
			{
				WorkflowId = selectedDefinition.Id,
				Configurations = new
				{
					encoded_picture = file,
				}
			}, new FieldSet
			{
				Fields = [
				nameof(Model.WorkflowExecution.Id),
				nameof(Model.WorkflowExecution.WorkflowId),
				]
			});
			// TODO: check if a builder here has meaning
			return workflowExecution.Id;
		}

		public async Task<string> GetInferenceStatusAsync(string executionId, IFieldSet fields = null)
		{
			this._logger.Debug(new MapLogEntry("getting inference status").And("executionId", executionId));
			await this._authorizationService.AuthorizeForce(Permission.CanGetImageInferenceStatus);
			var query = this._queryFactory.Query<WorkflowExecutionHttpQuery>().WorkflowIds(Common.WorkflowDefinitionId.AI_MODEL_REGISTRY_INFERENCE.ToString()).Id(executionId);

			AirflowDagExecution execution = await query.ByIdAsync();
			if (execution == null) throw new TerraNotFoundException(this._localizer["general_notFound", Common.WorkflowDefinitionKind.AiModelRegistryInference.ToString(), nameof(Model.WorkflowExecution)]);
			return execution.State;
		}

		public async Task<string> GetInferenceResultAsync(string executionId, IFieldSet fields = null)
		{
			this._logger.Debug(new MapLogEntry("getting inference result").And("executionId", executionId));
			await this._authorizationService.AuthorizeForce(Permission.CanGetImageInferenceResult);
			var xcomQuery = this._queryFactory.Query<WorkflowXcomEntryHttpQuery>()
				.WorkflowIds(Common.WorkflowDefinitionId.AI_MODEL_REGISTRY_INFERENCE.ToString())
				.TaskIds("infer") //TODO: make this more robust
				.WorkflowExecutionIds(executionId);
			xcomQuery.Page = new Paging
			{
				Size = 10,
				Offset = 0,
			};
			List<AirflowXcomEntry> xcoms = await xcomQuery.CollectAsync();
			if (xcoms == null || xcoms.Count == 0) throw new TerraNotFoundException(this._localizer["general_notFound", Common.WorkflowDefinitionKind.AiModelRegistryInference.ToString(), nameof(Model.WorkflowExecution)]);
			xcomQuery.XComKey(xcoms.FirstOrDefault().Key);
			var xcom = await xcomQuery.ByIdAsync();
			if (xcom == null) throw new TerraNotFoundException(this._localizer["general_notFound", Common.WorkflowDefinitionKind.AiModelRegistryInference.ToString(), nameof(Model.WorkflowExecution)]);
			return xcom.Value;
		}
	}
}
