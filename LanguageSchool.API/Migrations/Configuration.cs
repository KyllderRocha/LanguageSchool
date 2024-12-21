namespace LanguageSchool.API.Migrations
{
    using LanguageSchool.API.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LanguageSchool.API.Data.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LanguageSchool.API.Data.AppDbContext context)
        {
            var student1 = new Student
            {
                Name = "John Doe",
                CPF = "12345678901",
                DateOfBirth = new DateTime(2000, 5, 15)
            };

            var student2 = new Student
            {
                Name = "Jane Smith",
                CPF = "98765432100",
                DateOfBirth = new DateTime(1998, 7, 30)
            };

            context.Students.AddOrUpdate(
                s => s.CPF,
                student1,
                student2
            );

            context.SaveChanges();

            int student1Id = student1.Id;
            int student2Id = student2.Id;

            var class1 = new Class { Name = "Math", Code= "101" };
            var class2 = new Class { Name = "Science", Code = "102" };

            context.Classes.AddOrUpdate(
                c => c.Name,
                class1,
                class2
            );

            context.SaveChanges();

            int class1Id = class1.Id;      
            int class2Id = class2.Id;  

            context.Enrollments.AddOrUpdate(
                e => new { e.StudentId, e.ClassId },
                new Enrollment { StudentId = student1Id, ClassId = class1Id, EnrollmentDate = new DateTime(2024, 1, 10) },
                new Enrollment { StudentId = student2Id, ClassId = class2Id, EnrollmentDate = new DateTime(2024, 1, 15) }
            );

            context.SaveChanges();
        }
    }
}
