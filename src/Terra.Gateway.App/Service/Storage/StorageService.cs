using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Terra.Gateway.App.Service.Storage
{
	public class StorageService : IStorageService
	{
		private readonly StorageConfig _config;
		private readonly ILogger<StorageService> _logger;

		public StorageService(
			StorageConfig config,
			ILogger<StorageService> logger)
		{
			this._config = config;
			this._logger = logger;
		}

		public Task<List<String>> AllowedExtensions()
		{
			return Task.FromResult(this._config.UploadRules.AllowedExtensions);
		}

		public Task<long> MaxFileUploadSize()
		{
			return Task.FromResult(this._config.UploadRules.MaxFileSize);
		}

		private String GetExtensionWithoutDot(String extension)
		{
			String current = extension;
			if (extension.StartsWith(".")) current = extension.Substring(1);

			if (current.StartsWith(".")) return this.GetExtensionWithoutDot(current);

			return current;
		}

		public async Task<string> ConvertToBase64(IFormFile file)
		{
			using var memoryStream = new MemoryStream();
			await file.CopyToAsync(memoryStream);
			byte[] fileBytes = memoryStream.ToArray();
			return Convert.ToBase64String(fileBytes);
		}
	}
}
