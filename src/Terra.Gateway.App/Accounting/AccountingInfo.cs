
namespace Terra.Gateway.App.Accounting
{
	public enum KnownActions : short
	{
		None = 0,
		Query = 1,
		Persist = 2,
		Delete = 3,
		Invoke = 4,
		Rerun = 5,
		Infer = 6,
		InferStatus = 7,
		InferResult = 8,
	}

	public enum KnownUnits : short
	{
		Time = 0,
		Information = 1,
		Throughput = 2,
		Unit = 3,
	}

	public enum KnownTypes : short
	{
		Additive = 0,
		Subtractive = 1,
		Reset = 2,
	}

	public enum KnownResources : short
	{
		Collection = 0,
		User = 1,
		Vocabulary = 2,
		Workflow = 3,
		UserSettings = 4
	}

	public class AccountingInfo
	{
		public DateTime? TimeStamp { get; set; }
		public String ServiceId { get; set; }
		public KnownActions? Action {  get; set; }
		public String Resource { get; set; }
		public String UserId { get; set; }
		public String UserDelegate { get; set; }
		public String Value { get; set; }
		public KnownUnits? Measure { get; set; }
		public KnownTypes? Type { get; set; }
		public String Comment { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
	}
}
