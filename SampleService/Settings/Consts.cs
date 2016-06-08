namespace SampleService
{
	///<summary> Constants </summary>
	public static class Consts
	{
		public static class Program
		{
            /// <summary> [Sample Service] Service title </summary>
            public const string ServiceName = "Sample Service";
		}

		///<summary> Config keys </summary>
		public static class Config
		{
			/// <summary> [TimerDelay] Timer delay </summary>
			public const string TimerDelay = "TimerDelay";

			/// <summary> [OnExceptionDelay] Exception delay </summary>
			public const string OnExceptionDelay = "OnExceptionDelay";

			///<summary> [:] Separator for delay strings </summary>
			public const char DelayStringSeparator = ':';
		}
	}
}
