using Recruitment_Process_System_HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recruitment_Process_System_HR.Controllers
{
    public class AdminController : Controller
    {
        private RecruitmentEntities db = new RecruitmentEntities();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //login
        public ActionResult Login(Employee emp)
        {
            if (ModelState.IsValid)
            {
                var employee = db.Employees.Where(s => s.EmployeeCode.Equals(emp.EmployeeCode)
                && s.Password.Equals(emp.Password)).FirstOrDefault();
                if (employee != null)
                {
                    Session["username"] = employee.EmployeeCode;
                    Session["fullname"] = employee.Name;
                    Session["type"] = employee.Position;
                    return RedirectToAction("Index", "Interviews");
                }
                else
                {
                    ViewBag.error = "wrong username or password";
                }
            }
            return View(emp);
            //var pass = GetMD5(password);
        }

        //logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }
        public ActionResult Index()
        {
            
            return View();
        }
    }
}