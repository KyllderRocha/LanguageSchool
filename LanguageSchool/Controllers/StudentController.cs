using LanguageSchool.Models;
using LanguageSchool.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LanguageSchool.Controllers
{
    public class StudentController : Controller
    {
        StudentService _studentService = new StudentService();
        ClassService _classService = new ClassService();
        EnrollmentService _enrollmentService = new EnrollmentService();

        public async Task<ActionResult> Index()
        {
            if (TempData["CreateSuccess"] != null)
                ViewBag.CreateSuccess = TempData["CreateSuccess"];

            if (TempData["DeleteSuccess"] != null)
                ViewBag.DeleteSuccess = TempData["DeleteSuccess"];

            var Students = await _studentService.GetAll();
            return View(Students);
        }

        public async Task<ActionResult> Create(Student student = null)
        {
            if (student == null)
                student = new Student();

            if (TempData["CreateSuccess"] != null)
                    ViewBag.CreateSuccess = TempData["CreateSuccess"];

            ViewBag.AvailableClasses = await _classService.GetAllAvailable();

            return View(student);
        }


        [HttpPost]
        public async Task<ActionResult> SaveCreate(Student model)
        {
            TempData["CreateSuccess"] = false;
            if (ModelState.IsValid)
            {
                var success = await _studentService.CreateStudent(model);
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
                success = await _studentService.DeleteStudent(id);

            TempData["DeleteSuccess"] = success;
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var Class = await _studentService.GetById(id);
            if (Class == null)
            {
                return HttpNotFound();
            }

            if (TempData["EditSuccess"] != null)
                ViewBag.EditSuccess = TempData["EditSuccess"];

            ViewBag.AvailableClasses = await _enrollmentService.GetClassesNotForStudent(id);
            ViewBag.EnrollmentsClasses = await _enrollmentService.GetEnrollmentsByStudentId(id);

            return View(Class);
        }

        [HttpPost]
        public async Task<ActionResult> SaveEdit(Student model)
        {
            TempData["EditSuccess"] = false;
            if (ModelState.IsValid)
            {
                var sucesso = await _studentService.EditStudent(model);
                TempData["EditSuccess"] = sucesso;
            }
            return RedirectToAction("Edit", model);
        }
    }
}