using Cite.Tools.FieldSet;
using Cite.Tools.Logging;
using Cite.Tools.Logging.Extensions;
using Microsoft.Extensions.Logging;

namespace Terra.Gateway.App.Service.Version
{
	public class VersionInfoService : IVersionInfoService
	{
		private readonly ILogger<VersionInfoService> _logger;

		public VersionInfoService(
			ILogger<VersionInfoService> logger)
		{
			this._logger = logger;
		}

		public async Task<List<Model.VersionInfo>> CurrentAsync(IFieldSet fields = null)
		{
			fields = fields ?? new FieldSet(nameof(Model.VersionInfo.Key), nameof(Model.VersionInfo.Version), nameof(Model.VersionInfo.DeployedAt));
			this._logger.Debug(new MapLogEntry("current").And("type", nameof(App.Model.VersionInfo)).And("fields", fields));


			List<Data.VersionInfo> latests = new List<Data.VersionInfo>();

			List<Model.VersionInfo> models = this.ToModel(latests, fields);
			return models;
		}

		private List<Model.VersionInfo> ToModel(List<Data.VersionInfo> datas, IFieldSet fields)
		{
			List<Model.VersionInfo> models = new List<Model.VersionInfo>();
			foreach (Data.VersionInfo item in datas)
			{
				Model.VersionInfo m = new Model.VersionInfo();
				if (fields.HasField(nameof(Model.VersionInfo.Key))) m.Key = item.Key;
				if (fields.HasField(nameof(Model.VersionInfo.Version))) m.Version = item.Version;
				if (fields.HasField(nameof(Model.VersionInfo.ReleasedAt))) m.ReleasedAt = item.ReleasedAt;
				if (fields.HasField(nameof(Model.VersionInfo.DeployedAt))) m.DeployedAt = item.DeployedAt;
				if (fields.HasField(nameof(Model.VersionInfo.Description))) m.Description = item.Description;

				models.Add(m);
			}
			return models;
		}
	}
}
