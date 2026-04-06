using Microsoft.Extensions.Logging;

namespace Terra.Gateway.App.Accounting
{
	public class AccountingLogConfig
	{
		public Boolean Enable { get; set; }
		public LogLevel Level { get; set; }
		public String ServiceId { get; set; }
	}
}
