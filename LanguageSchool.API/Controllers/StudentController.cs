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

        public StudentController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<Student> GetAll()
        {
            return _unitOfWork.Students.GetAll();
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var student = _unitOfWork.Students.GetById(id);
            if (student == null)
                return NotFound();

            return Ok(student); 
        }

        [HttpPost]
        public IHttpActionResult Create([FromBody] StudentForm student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exists = _unitOfWork.Students.GetAll().Any(s => s.CPF == student.CPF);
            if (exists)
                return BadRequest("A student with this CPF already exists.");

            if (student.ClassId == 0)
                return BadRequest("The student needs a first class to be created.");

            _unitOfWork.Students.Add(new Student(student));
            _unitOfWork.Enrollments.Add(new Enrollment { StudentId = student.Id, ClassId = student.ClassId, EnrollmentDate = DateTime.Now });

            if (!_unitOfWork.SaveChanges())
                return BadRequest("Error to Save.");

            return CreatedAtRoute("DefaultApi", new { id = student.Id }, student); 
        }

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

            if (!_unitOfWork.SaveChanges())
                return BadRequest("Error to Save.");

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var student = _unitOfWork.Students.GetById(id);
            if (student == null)
                return NotFound();

            _unitOfWork.Students.Delete(student);

            if (!_unitOfWork.SaveChanges())
                return BadRequest("Error to Save.");

            return Ok(student);
        }
    }
}