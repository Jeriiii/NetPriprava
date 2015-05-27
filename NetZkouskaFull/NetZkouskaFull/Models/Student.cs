using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetZkouskaFull.Models
{
	public class Student
	{
		public int StudentID { get; set; }

		public string Name { get; set; }

		public string Lastname { get; set; }

		public string StudentIdentity { get; set; }

		public virtual ICollection<Exam> Exams { get; set; }

		public virtual ICollection<Work> Works { get; set; }
	}
}