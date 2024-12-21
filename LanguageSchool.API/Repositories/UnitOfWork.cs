using LanguageSchool.API.Data;
using LanguageSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageSchool.API.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private readonly AppDbContext _context;

        public GenericRepository<Student> Students { get; }
        public GenericRepository<Class> Classes { get; }
        public GenericRepository<Enrollment> Enrollments { get; }

        public UnitOfWork()
        {
            _context = new AppDbContext();
            Students = new GenericRepository<Student>(_context);
            Classes = new GenericRepository<Class>(_context);
            Enrollments = new GenericRepository<Enrollment>(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}