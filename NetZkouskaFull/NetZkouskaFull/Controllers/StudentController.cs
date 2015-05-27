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

namespace NetZkouskaFull.Controllers
{
	public class StudentController : Controller
	{
		private NetContext db = new NetContext();

		// GET: Student
		public ActionResult Index(string sortOrder)
		{
			ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewBag.LastnameSortParm = sortOrder == "Lastname" ? "lastname_desc" : "Lastname";
			ViewBag.SISortParm = sortOrder == "SI" ? "si_desc" : "SI";
			ViewBag.HasWorkDownSortParm = sortOrder == "HasWorkDown" ? "hasWorkDown_desc" : "HasWorkDown";
			ViewBag.GradeSortParm = sortOrder == "Grade" ? "grade_desc" : "Grade";
			ViewBag.GradePointsSortParm = sortOrder == "GradePoints" ? "gradePoints_desc" : "GradePoints";

			var students = from s in db.Students
						   select s;

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
