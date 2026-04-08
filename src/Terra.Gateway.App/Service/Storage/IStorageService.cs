namespace Terra.Gateway.App.Service.Storage
{
	public interface IStorageService
	{
		Task<List<string>> AllowedExtensions();
		Task<long> MaxFileUploadSize();
	}
}