using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageSchool.API.Models
{
    public class Enrollment
    {
        public int Id { get; set; } 
        public int StudentId { get; set; } 
        public int ClassId { get; set; } 
        public DateTime EnrollmentDate { get; set; }

        public virtual Student Student { get; set; } = null;
        public virtual Class Class { get; set; } = null;
    }

}