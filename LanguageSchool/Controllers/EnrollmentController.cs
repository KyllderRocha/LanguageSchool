using LanguageSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LanguageSchool.Controllers
{
    public class EnrollmentController : Controller
    {
        public ActionResult Index()
        {
            var Enrollments = new List<Enrollment>
            {
                new Enrollment { Id = 1, ClassId = 1, StudentId = 1, EnrollmentDate = DateTime.UtcNow },
                new Enrollment { Id = 2, ClassId = 1, StudentId = 2, EnrollmentDate = DateTime.UtcNow },
                new Enrollment { Id = 3, ClassId = 2, StudentId = 2, EnrollmentDate = DateTime.UtcNow }
            };
            return View(Enrollments);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}