using NetZkouskaFull.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NetZkouskaFull.DAL
{
	public class NetInit : System.Data.Entity.DropCreateDatabaseAlways<NetContext>
	{
		protected override void Seed(NetContext context)
		{
			SaveToXml();
			var students =  LoadToXml();

			students.Add(new Student{Name="Meredith",Lastname="Alonso",StudentIdentity="a10b0618p"});
			students.Add(new Student{Name="Arturo",Lastname="Anand",StudentIdentity="a10b0618p"});
			students.Add(new Student{Name="Gytis",Lastname="Barzdukas",StudentIdentity="a10b0618p"});
			students.Add(new Student{Name="Carson",Lastname="Alexander",StudentIdentity="a10b0618p"});

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
	

	public void SaveToXml()
		{
			// vytvor dokument
			XDocument doc = new XDocument();
			
			// vytvor element "Studenti" a pridej do dokumentu
			XElement xmlStudents = new XElement("Studenti");
			doc.Add(xmlStudents);

			var students = new List<Student>
			{
			new Student{Name="Carson",Lastname="Alexander",StudentIdentity="a10b0618p"},
			new Student{Name="Neal",Lastname="Jackson",StudentIdentity="a10b0618p"},
			new Student{Name="Pepa",Lastname="Omáčka",StudentIdentity="a10b0618p"},
			new Student{Name="Honza",Lastname="Skočdopole",StudentIdentity="a10b0618p"}
			};

			Random r = new Random();
			// pro kazdeho studenta vytvor element a vnorene elementy a napln hodnotami
			foreach (var student in students)
			{
				xmlStudents.Add(new XElement("Student",
					new XElement("Jmeno", student.Name),
					new XElement("Prijmeni", student.Lastname),
					new XElement("ID", student.StudentIdentity)
				));
			}

			doc.Save("D:\\škola\\netZkouskaFull\\data.xml");
		}
				  
		public List<Student> LoadToXml()
		{
			List<Student> students =new List<Student>();

			// vytvor dokument
			XDocument configXML = XDocument.Load("D:\\škola\\netZkouskaFull\\data.xml");

			// vytvor element "Studenti" a pridej do dokumentu
			var xmlStudents = from c in configXML.Descendants("Studenti").Descendants("Student") select c;
			

			// pro kazdeho studenta vytvor element a vnorene elementy a napln hodnotami
			foreach (var student in xmlStudents)
			{
				string name = (string) student.Descendants("Jmeno").FirstOrDefault();
				string lastname = (string)student.Descendants("Prijmeni").FirstOrDefault();
				string id = (string)student.Descendants("ID").FirstOrDefault();


				Student st = new Student { Name = name, Lastname = lastname, StudentIdentity = id };

				students.Add(st);
			}

			return students;
		}
	}
}