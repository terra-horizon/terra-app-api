using Microsoft.Extensions.DependencyInjection;

namespace Terra.Gateway.App.Service.UserSettings
{
	public static class Extensions
	{
		public static IServiceCollection AddUserSettingsServices(this IServiceCollection services)
		{
			services
				.AddScoped<IUserSettingsService, UserSettingsService>();

			return services;
		}
	}
}
