namespace Common.Logging
{
	/// <summary>
	/// Abstrakta klasa za logovanje
	/// </summary>
	public abstract class LogBase
	{
		/// <summary>
		/// Sadrzi lock objekat za zakljucavanje resursa, da ne dolazi do preplitanja niti
		/// </summary>
		protected readonly object lockObj = new object();

		public abstract void Log(Operation operation, string msg);
	}

	public enum Operation
	{
		Info = 0,
		Warning = 1,
		Error = 2
	}
}