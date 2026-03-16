using Cite.Tools.Data.Builder;
using Cite.Tools.Data.Censor;
using Cite.Tools.Data.Query;
using Cite.Tools.FieldSet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Terra.Gateway.App.Accounting;
using Terra.Gateway.App.ErrorCode;
using Terra.Gateway.App.Exception;
using Terra.Gateway.App.Query;
using Terra.Gateway.App.Service.Airflow;

namespace Terra.Gateway.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly CensorFactory _censorFactory;
        private readonly ErrorThesaurus _errors;
        private readonly BuilderFactory _builderFactory;
        private readonly QueryFactory _queryFactory;
        private readonly IAccountingService _accountingService;
        private readonly IStringLocalizer<Terra.Gateway.Resources.MySharedResources> _localizer;
        private readonly IAirflowService _airflowService;

        public TestController(
            ILogger<TestController> logger,
            CensorFactory censorFactory,
            ErrorThesaurus errors,
            BuilderFactory builderFactory,
            QueryFactory queryFactory,
            IAccountingService accountingService,
            IStringLocalizer<Terra.Gateway.Resources.MySharedResources> localizer,
            IAirflowService airflowService)
        {
            _logger = logger;
            _censorFactory = censorFactory;
            _errors = errors;
            _builderFactory = builderFactory;
            _queryFactory = queryFactory;
            _accountingService = accountingService;
            _localizer = localizer;
            _airflowService = airflowService;
        }

        [HttpGet("test/{hours}")]
        public async Task<IActionResult> BusyBox([FromRoute] int hours)
        {
            List<App.Service.Airflow.Model.AirflowDag> definitions = await this._queryFactory.Query<WorkflowDefinitionHttpQuery>()
                .Kinds(App.Common.WorkflowDefinitionKind.BUSYBOX)
                .ExcludeStaled(true)
                .CollectAsync();

            if (definitions == null || definitions.Count == 0) throw new DGNotFoundException(this._localizer["general_notFound", App.Common.WorkflowDefinitionKind.BUSYBOX.ToString(), nameof(App.Model.WorkflowDefinition)]);
            if (definitions.Count > 1) throw new DGFoundManyException(this._localizer["general_nonUnique", App.Common.WorkflowDefinitionKind.BUSYBOX.ToString(), nameof(App.Model.WorkflowDefinition)]);
            App.Service.Airflow.Model.AirflowDag selectedDefinition = definitions.FirstOrDefault();
            _ = await this._airflowService.ExecuteWorkflowAsync(new App.Model.WorkflowExecutionArgs
            {
                WorkflowId = selectedDefinition.Id,
                Configurations = new
                {
                   forHours = hours,
                }
            }, new FieldSet
            {
                Fields = [
                nameof(App.Model.WorkflowExecution.Id),
                nameof(App.Model.WorkflowExecution.WorkflowId),
                ]
            });
            return Ok();
        }
    }
}
