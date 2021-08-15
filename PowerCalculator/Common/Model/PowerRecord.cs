using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
	public class PowerRecord
	{
		public int Hour { get; set; }
		public DateTime Date { get; set; }
		public int Load { get; set; }
		public string Region { get; set; }
		public DateTime Timestamp { get; set; }

		public PowerRecord()
		{
		}

		public PowerRecord(int hour, DateTime date, int load, string region, DateTime timestamp)
		{
			Hour = hour;
			Date = date;
			Load = load;
			Region = region;
			Timestamp = timestamp;
		}
	}
}
