using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Recruitment_Process_System_HR.Models;

namespace Recruitment_Process_System_HR.Controllers
{
    public class VacanciesController : Controller
    {
        private RecruitmentEntities db = new RecruitmentEntities();

        // GET: Vacancies
        public ActionResult Index()
        {
            var vacancies = db.Vacancies.Include(v => v.Department).Include(v => v.Employee);
            return View(vacancies.ToList());
        }

        // GET: Vacancies/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacancy vacancy = db.Vacancies.Find(id);
            if (vacancy == null)
            {
                return HttpNotFound();
            }
            return View(vacancy);
        }

        // GET: Vacancies/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentCode = new SelectList(db.Departments, "DepartmentCode", "DepartmentName");
            ViewBag.EmployeeCode = new SelectList(db.Employees, "EmployeeCode", "DepartmentCode");
            return View();
        }

        // POST: Vacancies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VacId,EmployeeCode,DateBegin,status,Title,Description,RequireDescription,Benefit,Quantity,DepartmentCode,DateOfChangeStatus,FulfillDate")] Vacancy vacancy)
        {
            if (ModelState.IsValid)
            {
                vacancy.status = "Open";
                vacancy.DateBegin= DateTime.Now;
                db.Vacancies.Add(vacancy);
                //db.SaveChanges();
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentCode = new SelectList(db.Departments, "DepartmentCode", "DepartmentName", vacancy.DepartmentCode);
            ViewBag.EmployeeCode = new SelectList(db.Employees, "EmployeeCode", "DepartmentCode", vacancy.EmployeeCode);
            return View(vacancy);
        }

        // GET: Vacancies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacancy vacancy = db.Vacancies.Find(id);
            if (vacancy == null)
            {
                return HttpNotFound();
            }

            ViewBag.DepartmentCode = new SelectList(db.Departments, "DepartmentCode", "DepartmentName", vacancy.DepartmentCode);
            ViewBag.EmployeeCode = new SelectList(db.Employees, "EmployeeCode", "DepartmentCode", vacancy.EmployeeCode);
            //Creating generic list
            List<SelectListItem> ObjList = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Open", Value = "Open" },
                new SelectListItem { Text = "Close", Value = "Close" },

            };
            //Assigning generic list to ViewBag
            ViewBag.Locations = ObjList;

            return View(vacancy);
        }

        // POST: Vacancies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VacId,EmployeeCode,DateBegin,status,Title,Description,RequireDescription,Benefit,Quantity,DepartmentCode,DateOfChangeStatus,FulfillDate")] Vacancy vacancy)
        {
            if (ModelState.IsValid)
            {
                vacancy.DateOfChangeStatus = DateTime.Now;
                db.Entry(vacancy).State = EntityState.Modified;
                //db.SaveChanges();
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentCode = new SelectList(db.Departments, "DepartmentCode", "DepartmentName", vacancy.DepartmentCode);
            ViewBag.EmployeeCode = new SelectList(db.Employees, "EmployeeCode", "DepartmentCode", vacancy.EmployeeCode);
            return View(vacancy);
        }

        // GET: Vacancies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacancy vacancy = db.Vacancies.Find(id);
            if (vacancy == null)
            {
                return HttpNotFound();
            }
            return View(vacancy);
        }

        // POST: Vacancies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Vacancy vacancy = db.Vacancies.Find(id);
            db.Vacancies.Remove(vacancy);
            //db.SaveChanges();
            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
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
