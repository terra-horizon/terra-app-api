using Cite.Tools.FieldSet;
using Terra.Gateway.App.Model;

namespace Terra.Gateway.App.Service.UserSettings
{
	public interface IUserSettingsService
	{
		Task<Model.UserSettings> PersistAsync(UserSettingsPersist model, IFieldSet fields = null);
		Task DeleteAsync(Guid id);
		Task DeleteAsync(List<Guid> ids);
	}
}
