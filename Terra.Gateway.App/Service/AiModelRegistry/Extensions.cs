using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Terra.Gateway.App.Service.AiModelRegistry
{
	public static class Extensions
	{
		public static IServiceCollection AddAiModelRegistryServices(this IServiceCollection services, IConfigurationSection dataManagementSection)
		{
			services.AddScoped<IAiModelRegistryService, AiModelRegistryService>();
			return services;
		}
	}
}
