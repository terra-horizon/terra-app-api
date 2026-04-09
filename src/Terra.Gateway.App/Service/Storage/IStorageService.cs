using Microsoft.AspNetCore.Http;

namespace Terra.Gateway.App.Service.Storage
{
	public interface IStorageService
	{
		Task<List<string>> AllowedExtensions();
		Task<long> MaxFileUploadSize();
		Task<string> ConvertToBase64(IFormFile file);
	}
}