using LanguageSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace LanguageSchool.API.Data
{
    using System.Data.Entity;

    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection") 
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Enrollment>()
                .HasRequired(m => m.Student)
                .WithMany(a => a.Enrollments)
                .HasForeignKey(m => m.StudentId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Enrollment>()
                .HasRequired(m => m.Class)
                .WithMany(t => t.Enrollments)
                .HasForeignKey(m => m.ClassId)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }
    }


}