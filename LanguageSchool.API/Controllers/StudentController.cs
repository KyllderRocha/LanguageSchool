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
    public class StudentController : ApiController
    {
        private readonly UnitOfWork _unitOfWork;

        // Constructor with Dependency Injection
        public StudentController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Student
        [HttpGet]
        public IEnumerable<Student> GetAll()
        {
            return _unitOfWork.Students.GetAll();
        }

        // GET: api/Student/5
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var student = _unitOfWork.Students.GetById(id);
            if (student == null)
                return NotFound(); // Returns 404 status

            return Ok(student); // Returns 200 status with the student data
        }

        // POST: api/Student
        [HttpPost]
        public IHttpActionResult Create([FromBody] Student student, [FromBody] Class Class)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // Returns 400 status with validation errors

            var exists = _unitOfWork.Students.GetAll().Any(s => s.CPF == student.CPF);
            if (exists)
                return BadRequest("A student with this CPF already exists.");

            if (Class.Id == 0)
                return BadRequest("The student needs a first class to be created.");

            _unitOfWork.Students.Add(student);
            _unitOfWork.Enrollments.Add(new Enrollment { StudentId = student.Id, ClassId = Class.Id, EnrollmentDate = new DateTime() });
            _unitOfWork.Save();

            return CreatedAtRoute("DefaultApi", new { id = student.Id }, student); // Returns 201 status
        }

        // PUT: api/Student/5
        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody] Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingStudent = _unitOfWork.Students.GetById(id);
            if (existingStudent == null)
                return NotFound();

            existingStudent.Name = student.Name;
            existingStudent.CPF = student.CPF;
            existingStudent.DateOfBirth = student.DateOfBirth;

            _unitOfWork.Students.Update(existingStudent);
            _unitOfWork.Save();

            return StatusCode(HttpStatusCode.NoContent); // Returns 204 status
        }

        // DELETE: api/Student/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var student = _unitOfWork.Students.GetById(id);
            if (student == null)
                return NotFound();

            _unitOfWork.Students.Delete(student);
            _unitOfWork.Save();

            return Ok(student); // Returns 200 status with the deleted student
        }
    }
}