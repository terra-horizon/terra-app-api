using Cite.Tools.Validation;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Terra.Gateway.App.Common.Validation;
using Terra.Gateway.App.ErrorCode;

namespace Terra.Gateway.App.Model
{
	public class TaskInstanceStatusArgs
	{
		public string WorkflowId { get; set; }
		public string TaskId { get; set; }
		public string WorkflowExecutionId { get; set; }

		public class TaskInstanceStatusArgsValidator : BaseValidator<TaskInstanceStatusArgs>
		{
			public TaskInstanceStatusArgsValidator(
				IStringLocalizer<Terra.Gateway.Resources.MySharedResources> localizer,
				ValidatorFactory validatorFactory,
				ILogger<TaskInstanceStatusArgsValidator> logger,
				ErrorThesaurus errors) : base(validatorFactory, logger, errors)
			{
				this._localizer = localizer;
			}

			private readonly IStringLocalizer<Terra.Gateway.Resources.MySharedResources> _localizer;

			protected override IEnumerable<ISpecification> Specifications(TaskInstanceStatusArgs item)
			{
				return [
					//workflow id must always be set
					this.Spec()
						.Must(() => !this.IsEmpty(item.WorkflowId))
						.FailOn(nameof(TaskInstanceStatusArgs.WorkflowId)).FailWith(this._localizer["validation_required", nameof(TaskInstanceStatusArgs.WorkflowId)]),
					//workflow execution id must always be set
					this.Spec()
						.Must(() => !this.IsEmpty(item.WorkflowExecutionId))
						.FailOn(nameof(TaskInstanceStatusArgs.WorkflowExecutionId)).FailWith(this._localizer["validation_required", nameof(TaskInstanceStatusArgs.WorkflowExecutionId)]),
					//task id must always be set
					this.Spec()
						.Must(() => item.TaskId != null)
						.FailOn(nameof(TaskInstanceStatusArgs.TaskId)).FailWith(this._localizer["validation_required", nameof(TaskInstanceStatusArgs.TaskId)]),
				];
			}
		}
	}
}
