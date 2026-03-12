
using System.ComponentModel;

namespace Terra.Gateway.App.Common
{
	public enum WorkflowRunState : short
	{
		[Description("Queued")]
		Queued = 0,
		[Description("Running")]
		Running = 1,
		[Description("Success")]
		Success = 2,
		[Description("Failed")]
		Failed = 3,
	}
}
