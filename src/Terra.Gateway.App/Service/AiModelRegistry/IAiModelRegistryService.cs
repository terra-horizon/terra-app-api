using Cite.Tools.FieldSet;

namespace Terra.Gateway.App.Service.AiModelRegistry
{
	public interface IAiModelRegistryService
	{
		Task InferAsync(string file, IFieldSet fields = null);
	}
}