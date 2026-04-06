using Cite.Tools.Validation;
using Terra.Gateway.App.Common.Validation;
using Terra.Gateway.App.ErrorCode;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Terra.Gateway.App.Model
{
	public class WorkflowExecutionArgs
	{
		public String WorkflowId { get; set; }
        public object Configurations { get; set; }

        public class WorkflowExecutionArgsValidator : BaseValidator<WorkflowExecutionArgs>
		{
			public WorkflowExecutionArgsValidator(
				IStringLocalizer<Terra.Gateway.Resources.MySharedResources> localizer,
				ValidatorFactory validatorFactory,
				ILogger<WorkflowExecutionArgsValidator> logger,
				ErrorThesaurus errors) : base(validatorFactory, logger, errors)
			{
				this._localizer = localizer;
			}

			private readonly IStringLocalizer<Terra.Gateway.Resources.MySharedResources> _localizer;

			protected override IEnumerable<ISpecification> Specifications(WorkflowExecutionArgs item)
			{
				return new ISpecification[] {
					//workflow id must always be set
					this.Spec()
						.Must(() => !this.IsEmpty(item.WorkflowId))
						.FailOn(nameof(WorkflowExecutionArgs.WorkflowId)).FailWith(this._localizer["validation_required", nameof(WorkflowExecutionArgs.WorkflowId)]),
				};
			}
		}
	}
}
