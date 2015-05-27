using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetZkouskaFull.Models
{
	public class Work
	{
		public int WorkID { get; set; }

		public int Points { get; set; }

		public string Comment { get; set; }

		public DateTime DownDate { get; set; }
	}
}