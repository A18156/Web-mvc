using Recruitment_Process_System_HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recruitment_Process_System_HR.Controllers
{
    public class HomeController : Controller
    {
        private RecruitmentEntities db = new RecruitmentEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View(db.Vacancies.ToList());
        }
        public ActionResult Apply()
        {
            //Creating generic list
            List<SelectListItem> ObjList = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Male", Value = "Male" },
                new SelectListItem { Text = "Female", Value = "Female" },

            };
            //Assigning generic list to ViewBag
            ViewBag.Locations = ObjList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Apply([Bind(Include = "ApId,Name,Birthday,Gender,Address,IdentifyCard,Phone,Email,Major,DateCreated,Status")] Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                applicant.Status = "Not in Process";
                applicant.DateCreated = DateTime.Now;
                db.Applicants.Add(applicant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicant);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}