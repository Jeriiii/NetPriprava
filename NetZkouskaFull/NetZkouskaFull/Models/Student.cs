using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Linq;

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

		[NotMapped]
		public string HasWorkDown { 
			get{
				IEnumerable Config = GetConfig("work");

				int workPoints = 0;
				foreach(XElement g in Config) {
					workPoints = (int) g.Descendants("workPoints").FirstOrDefault();
				}

				foreach(Work w in Works) {
					if(w.Points >= workPoints) {
						return "Ma";
					}
				}

				return "Nema";
			}
		}

		[NotMapped]
		public int Grade
		{
			get
			{
				IEnumerable Config = GetConfig("exam");

				int one = 0;
				int two = 0;
				int three = 0;
				foreach (XElement g in Config)
				{
					one = (int)g.Descendants("one").FirstOrDefault();
					two = (int)g.Descendants("two").FirstOrDefault();
					three = (int)g.Descendants("three").FirstOrDefault();
				}

				int bestScore = 0;
				foreach (Exam e in Exams)
				{
					bestScore = Math.Max(bestScore, e.Points);
				}

				if (bestScore >= one)
				{
					return 1;
				}
				if (bestScore >= two)
				{
					return 2;
				}
				if (bestScore >= three)
				{
					return 3;
				}

				return 4;
			}
		}

		[NotMapped]
		public int GradePoints
		{
			get
			{
				int bestScore = 0;
				foreach (Exam e in Exams)
				{
					bestScore = Math.Max(bestScore, e.Points);
				}

				return bestScore;
			}
		}



		private IEnumerable GetConfig(string ConfigName) {
			XDocument configXML = XDocument.Load("D:\\škola\\netZkouskaFull\\config.xml");

			IEnumerable configQuery = from c in configXML.Descendants("configs").Descendants(ConfigName)
							 select c;// getting ship node in ships XML

			return configQuery;
		}
	}
}