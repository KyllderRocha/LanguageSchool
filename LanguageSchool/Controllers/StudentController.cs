using LanguageSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LanguageSchool.Controllers
{
    public class StudentController : Controller
    {
        public ActionResult Index()
        {
            var Students = new List<Student>
            {
                new Student { Id = 1, Name = "John", CPF = "101201329391" },
                new Student { Id = 2, Name = "Jane", CPF = "213214124214" },
                new Student { Id = 3, Name = "Bob", CPF = "12321431241" }
            };
            return View(Students);
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