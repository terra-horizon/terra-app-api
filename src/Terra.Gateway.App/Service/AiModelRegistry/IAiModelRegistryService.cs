using Cite.Tools.FieldSet;

namespace Terra.Gateway.App.Service.AiModelRegistry
{
	public interface IAiModelRegistryService
	{
		Task<string> InferAsync(string file, IFieldSet fields = null);
		Task<string> GetInferenceStatusAsync(string executionId, IFieldSet fields = null);
		Task<string> GetInferenceResultAsync(string executionId, IFieldSet fields = null);
	}
}