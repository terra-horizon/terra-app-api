using Cite.Tools.Logging;
using Cite.Tools.Logging.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Terra.Gateway.App.Accounting;
using Terra.Gateway.App.Service.AiModelRegistry;

namespace Terra.Gateway.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		private readonly ILogger<TestController> _logger;
		private readonly IAccountingService _accountingService;
		private readonly IAiModelRegistryService _aiModelRegistryService;

		public TestController(
			ILogger<TestController> logger,
			IAccountingService accountingService,
			IAiModelRegistryService aiModelRegistryService)
		{
			this._logger = logger;
			this._accountingService = accountingService;
			this._aiModelRegistryService = aiModelRegistryService;
		}

		[HttpPost("infer")]
		[Authorize]
		public async Task<IActionResult> Infer([FromForm] IFormFile file)
		{
			this._logger.Debug(new MapLogEntry("infer").And("model", file));
			using var memoryStream = new MemoryStream();
			await file.CopyToAsync(memoryStream);
			var base64 = Convert.ToBase64String(memoryStream.ToArray());
			await this._aiModelRegistryService.InferAsync(base64);

			this._accountingService.AccountFor(KnownActions.Infer, KnownResources.Dataset.AsAccountable());
			this._accountingService.AccountFor(KnownActions.Invoke, KnownResources.Workflow.AsAccountable());

			return Ok();
		}
	}
}
