using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NetZkouskaFull.DAL;
using NetZkouskaFull.Models;
using System.Collections;

namespace NetZkouskaFull.Controllers
{
	public class StudentController : Controller
	{
		private NetContext db = new NetContext();

		// GET: Student
		public ActionResult Index(string sortOrder, string sLastname, string sName, string sSI, string sHasCreditDown, string sGrade)
		{


			/*** Vzhledavani ***/

			ViewBag.sHasCreditDown = new SelectList(new List<SelectListItem> {
				//new SelectListItem { Selected = true, Text = string.Empty, Value = "-1"},
				new SelectListItem { Selected = false, Text = "Ma", Value = "Ma"},
				new SelectListItem { Selected = false, Text = "Nema", Value = "Nema"}
			}, "Value", "Text", null);

			ViewBag.sGrade = new SelectList(new List<SelectListItem> {
				//new SelectListItem { Selected = true, Text = string.Empty, Value = "-1"},
				new SelectListItem { Selected = false, Text = "1", Value = "1"},
				new SelectListItem { Selected = false, Text = "2", Value = "2"},
				new SelectListItem { Selected = false, Text = "3", Value = "3"},
				new SelectListItem { Selected = false, Text = "4", Value = "4"},
			}, "Value", "Text", null);

			var students = from s in db.Students
						   select s;

			List<int> search = new List<int>();

			if (!String.IsNullOrEmpty(sLastname))
			{
				students = students.Where(s => s.Lastname.Contains(sLastname)
									   || s.Lastname.Contains(sLastname));
			}

			if (!String.IsNullOrEmpty(sName))
			{
				students = students.Where(s => s.Name.Contains(sName)
									   || s.Name.Contains(sName));
			}

			if (!String.IsNullOrEmpty(sSI))
			{
				students = students.Where(s => s.StudentIdentity.Contains(sSI)
									   || s.StudentIdentity.Contains(sSI));
			}

			if (!String.IsNullOrEmpty(sHasCreditDown))
			{
				foreach(Student s in students) {
					if (s.HasWorkDown.Equals(sHasCreditDown))
					{
						search.Add(s.StudentID);
					}
				}

				students = findStudents(students, search);
			}

			if (!String.IsNullOrEmpty(sGrade))
			{
				search = new List<int>();
				foreach (Student s in students)
				{
					if (s.Grade.ToString().Equals(sGrade))
					{
						search.Add(s.StudentID);
					}
				}

				students = findStudents(students, search);
			}

			/*** RAZENI / SORTOVANI ***/

			ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewBag.LastnameSortParm = sortOrder == "Lastname" ? "lastname_desc" : "Lastname";
			ViewBag.SISortParm = sortOrder == "SI" ? "si_desc" : "SI";
			ViewBag.HasWorkDownSortParm = sortOrder == "HasWorkDown" ? "hasWorkDown_desc" : "HasWorkDown";
			ViewBag.GradeSortParm = sortOrder == "Grade" ? "grade_desc" : "Grade";
			ViewBag.GradePointsSortParm = sortOrder == "GradePoints" ? "gradePoints_desc" : "GradePoints";

			List<Student> std = students.ToList();

			switch (sortOrder)
			{
				case "name_desc":
					students = students.OrderByDescending(s => s.Name);
					break;
				case "Lastname":
					students = students.OrderBy(s => s.Lastname);
					break;
				case "lastname_desc":
					students = students.OrderByDescending(s => s.Lastname);
					break;
				case "SI":
					students = students.OrderBy(s => s.StudentIdentity);
					break;
				case "si_desc":
					students = students.OrderByDescending(s => s.StudentIdentity);
					break;
				case "HasWorkDown":
					std.Sort(
						delegate(Student s1, Student s2)
						{
							return s1.HasWorkDown.Equals(s2.HasWorkDown) ? 1 : -1;
						}
					);

					return View(std);
					break;
				case "hasWorkDown_desc":
					std.Sort(
						delegate(Student s1, Student s2)
						{
							return s1.HasWorkDown.Equals(s2.HasWorkDown) ? -1 : 1;
						}
					);
					return View(std);
				case "Grade":
					std.Sort(
						delegate(Student s1, Student s2)
						{
							return s1.Grade > s2.Grade ? -1 : 1;
						}
					);
					return View(std);
					break;
				case "grade_desc":
					std.Sort(
						delegate(Student s1, Student s2)
						{
							return s1.Grade > s2.Grade ? 1 : -1;
						}
					);
					return View(std);
					break;
				case "GradePoints":
					std.Sort(
						delegate(Student s1, Student s2)
						{
							return s1.GradePoints > s2.GradePoints ? 1 : -1;
						}
					);
					return View(std);
					break;
				case "gradePoints_desc":
					std.Sort(
						delegate(Student s1, Student s2)
						{
							return s1.GradePoints > s2.GradePoints ? -1 : 1;
						}
					);
					return View(std);
					break;
				default:
					students = students.OrderBy(s => s.Name);
					break;
			}

			return View(students.ToList());
		}

		private IQueryable<Student> findStudents(IQueryable<Student> students, List<int> searchIDs)
		{
			return students.Where(s => (
					searchIDs.Contains(s.StudentID)
				)
			);
		}

		// GET: Student/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Student student = db.Students.Find(id);
			if (student == null)
			{
				return HttpNotFound();
			}
			return View(student);
		}

		// GET: Student/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Student/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "StudentID,Name,Lastname,StudentIdentity")] Student student)
		{
			if (ModelState.IsValid)
			{
				db.Students.Add(student);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(student);
		}

		// GET: Student/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Student student = db.Students.Find(id);
			if (student == null)
			{
				return HttpNotFound();
			}
			return View(student);
		}

		// POST: Student/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "StudentID,Name,Lastname,StudentIdentity")] Student student)
		{
			if (ModelState.IsValid)
			{
				db.Entry(student).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(student);
		}

		// GET: Student/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Student student = db.Students.Find(id);
			if (student == null)
			{
				return HttpNotFound();
			}
			return View(student);
		}

		// POST: Student/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Student student = db.Students.Find(id);
			db.Students.Remove(student);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
