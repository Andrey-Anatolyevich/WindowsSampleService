namespace SampleService
{
	///<summary> Constants </summary>
	public static class Consts
	{
		public static string ServiceName = "RitualService";
		public static string ServiceDescription = "Ritual integration service for Amazon SQS, AmoCRM, Callmart";
		public static string ServiceDisplayName = "Ritual integration service";

		///<summary> Config keys </summary>
		public static class Config
		{
			///<summary> [:] Separator for delay strings </summary>
			public const char DelayStringSeparator = ':';
		}
	}
}
