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
    [RoutePrefix("api/Enrollment")]
    public class EnrollmentController : ApiController
    {
        private readonly UnitOfWork _unitOfWork;

        public EnrollmentController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<Enrollment> GetAll()
        {
            return _unitOfWork.Enrollments.GetAll();
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var Enrollment = _unitOfWork.Enrollments.GetById(id);
            if (Enrollment == null)
                return NotFound(); 

            return Ok(Enrollment);
        }

        [HttpPost]
        public IHttpActionResult Create([FromBody] Enrollment Enrollment)
        {
            Enrollment.EnrollmentDate = DateTime.Now;
            if (!ModelState.IsValid)
                return BadRequest(ModelState); 

            _unitOfWork.Enrollments.Add(Enrollment);
            if (!_unitOfWork.SaveChanges())
                return BadRequest("Error to Save.");

            return CreatedAtRoute("DefaultApi", new { id = Enrollment.Id }, Enrollment); 
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
            if (!_unitOfWork.SaveChanges())
                return BadRequest("Error to Save.");

            return StatusCode(HttpStatusCode.NoContent); 
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var Enrollment = _unitOfWork.Enrollments.GetById(id);
            if (Enrollment == null)
                return NotFound();

            _unitOfWork.Enrollments.Delete(Enrollment);
            if (!_unitOfWork.SaveChanges())
                return BadRequest("Error to Save.");

            return Ok(Enrollment); 
        }

        [HttpGet]
        [Route("GetByStudentId/{studentId}")]
        public IEnumerable<Enrollment> GetByStudentId(int studentId)
        {
            return _unitOfWork.Enrollments.GetAll().Where(e => e.StudentId == studentId);
        }
        
        [HttpGet]
        [Route("GetByClassId/{classId}")]
        public IEnumerable<Enrollment> GetByClassId(int classId)
        {
            return _unitOfWork.Enrollments.GetAll().Where(e => e.ClassId == classId);
        }

        [HttpGet]
        [Route("GetStudentsNotInClass/{classId}")]
        public IEnumerable<Student> GetStudentsNotInClass(int classId)
        {
            var studentsInClass = _unitOfWork.Enrollments.GetAll().Where(e => e.ClassId == classId).Select(e => e.StudentId);

            return _unitOfWork.Students.GetAll().Where(s => !studentsInClass.Contains(s.Id));
        }

        [HttpGet]
        [Route("GetClassesNotForStudent/{studentId}")]
        public IEnumerable<Class> GetClassesNotForStudent(int studentId)
        {
            var classesForStudent = _unitOfWork.Enrollments.GetAll().Where(e => e.StudentId == studentId).Select(e => e.ClassId);
            return _unitOfWork.Classes.GetAll().Where(c => !classesForStudent.Contains(c.Id) && c.Enrollments.Count < 5);
        }

    }
}
