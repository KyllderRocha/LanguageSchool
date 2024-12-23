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
    [RoutePrefix("api/Class")]
    public class ClassController : ApiController
    {
        private readonly UnitOfWork _unitOfWork;

        public ClassController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<Class> GetAll()
        {
            return _unitOfWork.Classes.GetAll();
        }

        [HttpGet]
        [Route("Available")]
        public IEnumerable<Class> GetAvailableClasses()
        {
            return _unitOfWork.Classes.GetAll().Where(c => c.Enrollments.Count < 5);
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var Class = _unitOfWork.Classes.GetById(id);
            if (Class == null)
                return NotFound(); 

            return Ok(Class); 
        }

        [HttpPost]
        public IHttpActionResult Create([FromBody] Class Class)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exists = _unitOfWork.Classes.GetAll().Any(s => s.Code == Class.Code);
            if (exists)
                return BadRequest("A Class with this Code already exists.");

            _unitOfWork.Classes.Add(Class);
            if (!_unitOfWork.SaveChanges())
                return BadRequest("Error to Save.");

            return CreatedAtRoute("DefaultApi", new { id = Class.Id }, Class); 
        }

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
            if (!_unitOfWork.SaveChanges())
                return BadRequest("Error to Save.");

            return StatusCode(HttpStatusCode.NoContent); 
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var Class = _unitOfWork.Classes.GetById(id);
            if (Class == null)
                return NotFound();

            _unitOfWork.Classes.Delete(Class);
            if (!_unitOfWork.SaveChanges())
                return BadRequest("Error to Save.");

            return Ok(Class); 
        }
    }
}