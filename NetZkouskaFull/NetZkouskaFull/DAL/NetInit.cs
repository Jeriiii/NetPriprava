using NetZkouskaFull.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetZkouskaFull.DAL
{
	public class NetInit : System.Data.Entity.DropCreateDatabaseAlways<NetContext>
	{
		protected override void Seed(NetContext context)
		{
			Work w1 = new Work{Points = 45};

			Student s1 = new Student{Name="Carson",Lastname="Alexander",StudentIdentity="a10b0618p"};

			var students = new List<Student>
			{
			new Student{Name="Meredith",Lastname="Alonso",StudentIdentity="a10b0618p"},
			new Student{Name="Arturo",Lastname="Anand",StudentIdentity="a10b0618p"},
			new Student{Name="Gytis",Lastname="Barzdukas",StudentIdentity="a10b0618p"},
			new Student{Name="Carson",Lastname="Alexander",StudentIdentity="a10b0618p"}
			};

			Random r = new Random(75);
			foreach(Student s in students) {
				List<Exam> Exams = new List<Exam>();
				Exams.Add(new Exam { Points = r.Next(80), DownDate = DateTime.Parse("2015-01-02") });
				s.Exams = Exams;

				List<Work> Works = new List<Work>();
				Works.Add(new Work { Points = r.Next(80), DownDate = DateTime.Parse("2015-01-02"), Comment="" });
				s.Works = Works;
			}

			//new Student{Name="Yan",Lastname="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
			//new Student{Name="Peggy",Lastname="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
			//new Student{Name="Laura",Lastname="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
			//new Student{Name="Nino",Lastname="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}

			students.ForEach(s => context.Students.Add(s));
			context.SaveChanges();
		}
	}
}