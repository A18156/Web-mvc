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
    public class InterviewersController : Controller
    {
        private RecruitmentEntities db = new RecruitmentEntities();

        // GET: Interviewers
        public ActionResult Index()
        {
            var interviewers = db.Interviewers.Include(i => i.Employee).Include(i => i.Interview);
            return View(interviewers.ToList());
        }

        // GET: Interviewers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interviewer interviewer = db.Interviewers.Find(id);
            if (interviewer == null)
            {
                return HttpNotFound();
            }
            return View(interviewer);
        }

        // GET: Interviewers/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeCode = new SelectList(db.Employees, "EmployeeCode", "EmployeeCode");
            ViewBag.InterID = new SelectList(db.Interviews, "InterID", "InterID");
            return View();
        }

        // POST: Interviewers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InterCode,Status,InterID,EmployeeCode")] Interviewer interviewer)
        {
            if (ModelState.IsValid)
            {
                db.Interviewers.Add(interviewer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeCode = new SelectList(db.Employees, "EmployeeCode", "EmployeeCode", interviewer.EmployeeCode);
            ViewBag.InterID = new SelectList(db.Interviews, "InterID", "InterID", interviewer.InterID);
            return View(interviewer);
        }

        // GET: Interviewers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interviewer interviewer = db.Interviewers.Find(id);
            if (interviewer == null)
            {
                return HttpNotFound();
            }

            ViewBag.EmployeeCode = new SelectList(db.Employees, "EmployeeCode", "DepartmentCode", interviewer.EmployeeCode);
            ViewBag.InterID = new SelectList(db.Interviews, "InterID", "Location", interviewer.InterID);
            //Creating generic list
            List<SelectListItem> ObjList = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Accept", Value = "Accept" },
                new SelectListItem { Text = "Rejected", Value = "Rejected" },
            };
            //Assigning generic list to ViewBag
            ViewBag.Locations = ObjList;
            return View(interviewer);
        }

        // POST: Interviewers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InterCode,Status,InterID,EmployeeCode")] Interviewer interviewer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interviewer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeCode = new SelectList(db.Employees, "EmployeeCode", "DepartmentCode", interviewer.EmployeeCode);
            ViewBag.InterID = new SelectList(db.Interviews, "InterID", "Location", interviewer.InterID);
            return View(interviewer);
        }

        // GET: Interviewers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interviewer interviewer = db.Interviewers.Find(id);
            if (interviewer == null)
            {
                return HttpNotFound();
            }
            return View(interviewer);
        }

        // POST: Interviewers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Interviewer interviewer = db.Interviewers.Find(id);
            db.Interviewers.Remove(interviewer);
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
