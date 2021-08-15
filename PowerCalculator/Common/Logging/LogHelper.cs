namespace Common.Logging
{
	/// <summary>
	/// Staticna klasa za logovanje, ako hocemo da imamo vise razlicitih logera...
	/// Za bazu, za UI, za sistem ovde mozemo da pozivamo te logere u metodi Log
	/// </summary>
	public static class LogHelper
	{
		private static LogBase logger = null;

		public static void Log(Operation operation, string msg)
		{
			logger = new Logger();
			logger.Log(operation, msg);
		}
	}
}