using Newtonsoft.Json;

namespace Terra.Gateway.App.Service.AAI.Model
{
	public class RoleMapping
	{
		[JsonProperty("id")]
		public String Id { get; set; }
		[JsonProperty("name")]
		public String Name { get; set; }
	}
}
