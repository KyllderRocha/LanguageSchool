using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageSchool.API.Models
{
    public class Student
    {

        public Student()
        {
        }

        public Student(StudentForm student)
        {
            Name = student.Name;
            CPF = student.CPF;
            DateOfBirth = student.DateOfBirth;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(11)]
        [Index(IsUnique = true)] 
        public string CPF { get; set; } = string.Empty; 
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}