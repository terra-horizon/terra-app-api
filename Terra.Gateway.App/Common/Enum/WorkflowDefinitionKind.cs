
using System.ComponentModel;

namespace Terra.Gateway.App.Common
{
	public enum WorkflowDefinitionKind : short
	{
		[Description("Dataset Onboarding")]
		DatasetOnboarding = 0,
		[Description("Dataset Profiling")]
		DatasetProfiling = 1,
	}
}
