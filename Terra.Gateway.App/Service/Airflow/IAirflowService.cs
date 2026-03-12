using Cite.Tools.FieldSet;
using Terra.Gateway.App.Model;

namespace Terra.Gateway.App.Service.Airflow
{
	public interface IAirflowService
	{
		Task<App.Model.WorkflowExecution> ExecuteWorkflowAsync(WorkflowExecutionArgs args, IFieldSet fields);
		Task<List<App.Model.WorkflowTaskInstance>> ReRunWorkflowTasksAsync(TaskInstanceDownstreamExecutionArgs args, IFieldSet fields);
	}
}

