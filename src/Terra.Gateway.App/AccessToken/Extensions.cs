using Microsoft.Extensions.DependencyInjection;

namespace Terra.Gateway.App.AccessToken
{
	public static class Extensions
	{
		public static IServiceCollection AddAccessTokenServices(this IServiceCollection services)
		{
			services
				.AddScoped<RequestTokenIntercepted>()
				.AddTransient<IAccessTokenService, AccessTokenService>();

			return services;
		}
	}
}
