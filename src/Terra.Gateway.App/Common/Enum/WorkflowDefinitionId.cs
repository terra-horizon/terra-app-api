using System.ComponentModel;

namespace Terra.Gateway.App.Common
{
	public enum WorkflowDefinitionId
	{
		[Description("Busybox")]
		BUSYBOX = 0,
		[Description("AI Model Registry Inference")]
		AI_MODEL_REGISTRY_INFERENCE = 1,
	}
}
