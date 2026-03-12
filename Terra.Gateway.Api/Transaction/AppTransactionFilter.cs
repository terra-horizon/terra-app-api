using Cite.WebTools.Data.Transaction;
using Terra.Gateway.App.Data;

namespace Terra.Gateway.Api.Transaction
{
	public class AppTransactionFilter : TransactionFilter
	{
		public AppTransactionFilter(AppDbContext dbContext, ILogger<AppTransactionFilter> logger) : base(dbContext, logger) { }
	}
}
