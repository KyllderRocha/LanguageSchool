using LanguageSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LanguageSchool.Controllers
{
    public class ClassController : Controller
    {
        public ActionResult Index()
        {
            var Classes = new List<Class>
            {
                new Class { Id = 1, Code = "1001", Name = "Math" },
                new Class { Id = 2, Code = "1002", Name = "English" },
                new Class { Id = 3, Code = "1003", Name = "Science" }
            };
            return View(Classes);
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