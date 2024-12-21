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
    public class ClassController : ApiController
    {
        private readonly UnitOfWork _unitOfWork;

        // Constructor with Dependency Injection
        public ClassController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Class
        [HttpGet]
        public IEnumerable<Class> GetAll()
        {
            return _unitOfWork.Classes.GetAll();
        }

        // GET: api/Class/5
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var Class = _unitOfWork.Classes.GetById(id);
            if (Class == null)
                return NotFound(); // Returns 404 status

            return Ok(Class); // Returns 200 status with the Class data
        }

        // POST: api/Class
        [HttpPost]
        public IHttpActionResult Create([FromBody] Class Class)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // Returns 400 status with validation errors

            var exists = _unitOfWork.Classes.GetAll().Any(s => s.Code == Class.Code);
            if (exists)
                return BadRequest("A Class with this Code already exists.");

            _unitOfWork.Classes.Add(Class);
            _unitOfWork.Save();

            return CreatedAtRoute("DefaultApi", new { id = Class.Id }, Class); // Returns 201 status
        }

        // PUT: api/Class/5
        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody] Class Class)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingClass = _unitOfWork.Classes.GetById(id);
            if (existingClass == null)
                return NotFound();

            existingClass.Name = Class.Name;
            existingClass.Code = Class.Code;

            _unitOfWork.Classes.Update(existingClass);
            _unitOfWork.Save();

            return StatusCode(HttpStatusCode.NoContent); // Returns 204 status
        }

        // DELETE: api/Class/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var Class = _unitOfWork.Classes.GetById(id);
            if (Class == null)
                return NotFound();

            _unitOfWork.Classes.Delete(Class);
            _unitOfWork.Save();

            return Ok(Class); // Returns 200 status with the deleted Class
        }
    }
}