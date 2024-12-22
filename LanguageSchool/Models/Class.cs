using System.Collections.Generic;

namespace LanguageSchool.Models
{
    public class Class
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty; 

        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}