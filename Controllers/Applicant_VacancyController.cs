using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Recruitment_Process_System_HR.Models;

namespace Recruitment_Process_System_HR.Controllers
{
    public class Applicant_VacancyController : Controller
    {
        private RecruitmentEntities db = new RecruitmentEntities();

        // GET: Applicant_Vacancy
        public ActionResult Index()
        {
            var applicant_Vacancy = db.Applicant_Vacancy.Include(a => a.Applicant).Include(a => a.Vacancy);
            return View(applicant_Vacancy.ToList());
        }

        // GET: Applicant_Vacancy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant_Vacancy applicant_Vacancy = db.Applicant_Vacancy.Find(id);
            if (applicant_Vacancy == null)
            {
                return HttpNotFound();
            }
            return View(applicant_Vacancy);
        }

        // GET: Applicant_Vacancy/Create
        public ActionResult Create()
        {
            ViewBag.ApId = new SelectList(db.Applicants, "ApId", "ApId");
            ViewBag.VacID = new SelectList(db.Vacancies, "VacId", "VacId");
     
            return View();
        }

        // POST: Applicant_Vacancy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VAId,ApId,VacID,AttachesDate,Status")] Applicant_Vacancy applicant_Vacancy)
        {
            if (ModelState.IsValid)
            {
                applicant_Vacancy.Status = "Scheduled";
                db.Applicant_Vacancy.Add(applicant_Vacancy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApId = new SelectList(db.Applicants, "ApId", "ApId", applicant_Vacancy.ApId);
            ViewBag.VacID = new SelectList(db.Vacancies, "VacId", "VacId", applicant_Vacancy.VacID);
            return View(applicant_Vacancy);
        }

        // GET: Applicant_Vacancy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant_Vacancy applicant_Vacancy = db.Applicant_Vacancy.Find(id);
            if (applicant_Vacancy == null)
            {
                return HttpNotFound();
            }
            //Creating generic list
            List<SelectListItem> ObjList = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Interview Scheduled", Value = "Interview Scheduled" },
                new SelectListItem { Text = "Rejected", Value = "Rejected" },
                new SelectListItem { Text = "Not Required", Value = "Not Required" },

            };
            //Assigning generic list to ViewBag
            ViewBag.Locations = ObjList;
            ViewBag.ApId = new SelectList(db.Applicants, "ApId", "Name", applicant_Vacancy.ApId);
            ViewBag.VacID = new SelectList(db.Vacancies, "VacId", "EmployeeCode", applicant_Vacancy.VacID);
            return View(applicant_Vacancy);
        }

        // POST: Applicant_Vacancy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VAId,ApId,VacID,AttachesDate,Status")] Applicant_Vacancy applicant_Vacancy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicant_Vacancy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApId = new SelectList(db.Applicants, "ApId", "Name", applicant_Vacancy.ApId);
            ViewBag.VacID = new SelectList(db.Vacancies, "VacId", "EmployeeCode", applicant_Vacancy.VacID);
            return View(applicant_Vacancy);
        }

        // GET: Applicant_Vacancy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant_Vacancy applicant_Vacancy = db.Applicant_Vacancy.Find(id);
            if (applicant_Vacancy == null)
            {
                return HttpNotFound();
            }
            return View(applicant_Vacancy);
        }

        // POST: Applicant_Vacancy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Applicant_Vacancy applicant_Vacancy = db.Applicant_Vacancy.Find(id);
            db.Applicant_Vacancy.Remove(applicant_Vacancy);
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

        [HttpPost, ActionName("sendInternviewMail")]
        [ValidateAntiForgeryToken]
        public ActionResult sendInternviewMail(int id)
        {
            try
            {
                Applicant applicants = db.Applicants.Find(id);
                Applicant_Vacancy av = db.Applicant_Vacancy.Find(id);
                // send mail
                // 1. build mail content
                MailMessage mail = new MailMessage();
                mail.To.Add(applicants.Email);
                mail.From = new MailAddress("ExmapleForDemo@gmail.com");
                mail.Subject = "\"Invite Interview\"";
                mail.Body = "<html><body><h1>Hi! </h1>Sent to <b>" + applicants.Name + "</b>"+"</body><html>";
                mail.IsBodyHtml = true;
                // send mail
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("ExmapleForDemo@gmail.com", "abc@a18156");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                // up status -> db
                av.Status = "Select"; 
                db.SaveChanges();
                TempData["message"] = "Send mail completed!";
                TempData["result"] = 1;
            }
            catch (Exception e)
            {
                TempData["message"] = "Send mail failed!";
                TempData["result"] = 0;
            }
            return RedirectToAction("Index", "Applicant_Vacancy");
        }
    }
}
