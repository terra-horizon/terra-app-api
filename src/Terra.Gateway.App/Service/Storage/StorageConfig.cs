namespace Terra.Gateway.App.Service.Storage
{
	public class StorageConfig
	{
		public FileRestrictions UploadRules { get; set; }
	}

	public class FileRestrictions
	{
		public long MaxFileSize { get; set; }
		public List<string> AllowedExtensions { get; set; }
	}
}
