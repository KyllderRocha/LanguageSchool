using LanguageSchool.API.Data;
using LanguageSchool.API.Models;
using LanguageSchool.API.Repositories;
using LanguageSchool.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace LanguageSchool.API.Controllers
{
    public class EnrollmentController : ApiController
    {
        private readonly UnitOfWork _unitOfWork;

        // Constructor with Dependency Injection
        public EnrollmentController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Enrollment
        [HttpGet]
        public IEnumerable<Enrollment> GetAll()
        {
            return _unitOfWork.Enrollments.GetAll();
        }

        // GET: api/Enrollment/5
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var Enrollment = _unitOfWork.Enrollments.GetById(id);
            if (Enrollment == null)
                return NotFound(); // Returns 404 status

            return Ok(Enrollment); // Returns 200 status with the Enrollment data
        }

        // POST: api/Enrollment
        [HttpPost]
        public IHttpActionResult Create([FromBody] Enrollment Enrollment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // Returns 400 status with validation errors

            _unitOfWork.Enrollments.Add(Enrollment);
            _unitOfWork.Save();

            return CreatedAtRoute("DefaultApi", new { id = Enrollment.Id }, Enrollment); // Returns 201 status
        }

        // PUT: api/Enrollment/5
        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody] Enrollment Enrollment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingEnrollment = _unitOfWork.Enrollments.GetById(id);
            if (existingEnrollment == null)
                return NotFound();

            existingEnrollment.StudentId = Enrollment.StudentId;
            existingEnrollment.ClassId = Enrollment.ClassId;

            _unitOfWork.Enrollments.Update(existingEnrollment);
            _unitOfWork.Save();

            return StatusCode(HttpStatusCode.NoContent); // Returns 204 status
        }

        // DELETE: api/Enrollment/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var Enrollment = _unitOfWork.Enrollments.GetById(id);
            if (Enrollment == null)
                return NotFound();

            _unitOfWork.Enrollments.Delete(Enrollment);
            _unitOfWork.Save();

            return Ok(Enrollment); // Returns 200 status with the deleted Enrollment
        }
    }
}