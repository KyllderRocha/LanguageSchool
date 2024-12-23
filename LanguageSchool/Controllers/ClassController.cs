using LanguageSchool.Models;
using LanguageSchool.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LanguageSchool.Controllers
{
    public class ClassController : Controller
    {
        ClassService _classService = new ClassService();
        EnrollmentService _enrollmentService = new EnrollmentService();

        public async Task<ActionResult> Index()
        {
            if (TempData["CreateSuccess"] != null)
                ViewBag.CreateSuccess = TempData["CreateSuccess"];

            if (TempData["DeleteSuccess"] != null)
                ViewBag.DeleteSuccess = TempData["DeleteSuccess"];

            var Classes = await _classService.GetAll();
            return View(Classes);
        }

        public ActionResult Create(Class Class = null)
        {

            if (Class == null)
                Class = new Class();

            if (TempData["CreateSuccess"] != null)
                ViewBag.CreateSuccess = TempData["CreateSuccess"];

            return View(Class);
        }

        [HttpPost]
        public async Task<ActionResult> SaveCreate(Class model)
        {
            TempData["CreateSuccess"] = false;
            if (ModelState.IsValid)
            {
                var success = await _classService.CreateClass(model);
                TempData["CreateSuccess"] = success;
                if (success)
                    return RedirectToAction("Index");
            }

            return RedirectToAction("Create", model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var success = false;
            if (ModelState.IsValid)
                success = await _classService.DeleteClass(id);

            TempData["DeleteSuccess"] = success;
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var Class = await _classService.GetById(id);
            if (Class == null)
            {
                return HttpNotFound();
            }

            if (TempData["EditSuccess"] != null)
                ViewBag.EditSuccess = TempData["EditSuccess"];

            ViewBag.AvailableStudents = await _enrollmentService.GetStudentsNotInClass(id);
            ViewBag.EnrollmentsStudents = await _enrollmentService.GetEnrollmentsByClassId(id);

            return View(Class);
        }

        [HttpPost]
        public async Task<ActionResult> SaveEdit(Class model)
        {
            TempData["EditSuccess"] = false;
            if (ModelState.IsValid)
            {
                var sucesso = await _classService.EditClass(model);
                TempData["EditSuccess"] = sucesso;
            }
            return RedirectToAction("Edit", model);
        }
    }
}