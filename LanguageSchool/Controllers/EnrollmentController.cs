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
    public class EnrollmentController : Controller
    {
        private readonly EnrollmentService _enrollmentService;

        public EnrollmentController()
        {
            _enrollmentService = new EnrollmentService();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Enrollment enrollment, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var success = await _enrollmentService.AddEnrollment(enrollment);
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index");
            }
            return View(enrollment);
        }

        public async Task<ActionResult> GetEnrollmentsByClassId(int classId)
        {
            var enrollments = _enrollmentService.GetEnrollmentsByClassId(classId);
            return View(enrollments);
        }

        public async Task<ActionResult> GetEnrollmentsByStudentId(int studentId)
        {
            var enrollments = _enrollmentService.GetEnrollmentsByStudentId(studentId);
            return View(enrollments);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteEnrollment(int id, string returnUrl)
        {
            var success = await _enrollmentService.DeleteEnrollment(id);
            TempData["DeleteEnrollmentSuccess"] = success;
            return Redirect(returnUrl);
        }
    }
}